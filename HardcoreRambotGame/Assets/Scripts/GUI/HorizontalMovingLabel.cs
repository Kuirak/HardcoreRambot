using UnityEngine;
using System.Collections;

public class HorizontalMovingLabel : AnimatedGUIControl {

	// The text to display
	public string Text { get; set; }
	
	// Moving velocity of the text
	public float Speed { get; set; }
	
	// Stopping position of the moving label.
	// For labels moving right, this is the final position of the right border.
	// For labels moving left, this is the final position of the left border.
	public float StopPosition { get; set; }
	
    // Custom gui skin for labels
    public GUISkin CustomSkin { get; set; }
    public string StyleName { get; set; }
	
	// Controls which border is aligned to the stop position.
	// i.e. If alignment is left, then the left border of the label is not allowed
	// to cross the "StopPosition" vertical line.
	public enum BorderAlignment 
	{
		LEFT,
		RIGHT
	};
	
	public BorderAlignment AlignedBorder { get; set; }

	//
	// Private
	//
	
	protected float lastLayoutTime;
	protected Rect textArea;
	protected float firstTick;
	
	// Time after first call to Tick() when to start displaying the labels.
	protected float displayAfter;
	
	public HorizontalMovingLabel(string text, float speed, Vector2 startPosition, float stopPosition, BorderAlignment alignedBorder, float displayAfter) : base()
	{
		this.textArea = new Rect(startPosition.x, startPosition.y, 0, 0);
		
		this.Text = text;
		this.Speed = speed;
		this.StopPosition = stopPosition;
		this.AlignedBorder = alignedBorder;
		this.displayAfter = displayAfter;

		this.lastLayoutTime = 0;
		this.firstTick = 0;
		
		this.StyleName = "label";
	}
	
	protected void CalculateFrame()
	{
		// Calculate the text width.
		Vector2 textSize = GetStyle().CalcSize(new GUIContent(this.Text));
		
		textArea.width = textSize.x;
		textArea.height = textSize.y;
	}

	// Return the style for this label. 
	protected GUIStyle GetStyle()
	{
		return GUIUtils.FindStyleOrDefault(this.StyleName, GUI.skin.label);
	}
	
	// Move the text by updating the internal values.
	protected void MoveText()
	{
		// First frame where we're drawn?
		if (this.lastLayoutTime == 0)
		{
			this.lastLayoutTime = Time.time;
		}
		
		float deltaTime = Time.time - this.lastLayoutTime;

		// Calculate new position and clip
		this.textArea.x = this.textArea.x + (this.Speed * deltaTime);
		
		if (this.AlignedBorder == BorderAlignment.LEFT)
		{
			// We align the left border, so we compare to textArea.x
			if (this.Speed > 0)
			{
				if (this.textArea.x > this.StopPosition)
				{
					this.textArea.x = this.StopPosition;
					this.AnimationDone = true;
				}
			}
			else
			{
				if (this.textArea.x < this.StopPosition)
				{
					this.textArea.x = this.StopPosition;
					this.AnimationDone = true;
				}
			}
		}
		else
		{
			// We align the left border, so we compare to textArea.x + width
			if (this.Speed > 0)
			{
				if (this.textArea.x + this.textArea.width > this.StopPosition)
				{
					this.textArea.x = this.StopPosition - this.textArea.width;
					this.AnimationDone = true;
				}
			}
			else
			{
				if (this.textArea.x + this.textArea.width < this.StopPosition)
				{
					this.textArea.x = this.StopPosition - this.textArea.width;
					this.AnimationDone = true;
				}
			}
		}
		
		this.lastLayoutTime = Time.time;
	}

	// Draw the label on screen
	protected void Draw()
	{
		GUI.Box(textArea, "");
		GUI.Label(textArea, this.Text, GetStyle());
	}
	
	public override void Tick()
	{
		if (firstTick == 0)
		{
			firstTick = Time.time;
		}
		
		if (Time.time < firstTick + displayAfter)
		{
			// display after time hasn't passed yet
			return;
		}
		
		GUISkin oldSkin = GUI.skin;
		
		if (CustomSkin != null)
        {
            GUI.skin = CustomSkin;
        }
		
		try
		{
			CalculateFrame();
			
			switch (Event.current.type)
			{
			case EventType.Layout:
				MoveText();
				break;
			default:
				break;
			}
			
			Draw();
		}
		finally
		{
            GUI.skin = oldSkin;
		}
	}
}
