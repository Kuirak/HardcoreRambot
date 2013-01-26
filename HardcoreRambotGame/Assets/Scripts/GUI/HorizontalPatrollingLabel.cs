using UnityEngine;
using System.Collections;

public class HorizontalPatrollingLabel : AnimatedGUIControl {

	// The text to display
	public string Text { get; set; }
	
	// Moving velocity of the text
	public float Speed { get; set; }
	
	// Stopping position of the moving label.
	// For labels moving right, this is the final position of the right border.
	// For labels moving left, this is the final position of the left border.
	public float StopPosition { get; set; }
	
	//
	// Private
	//
	
	protected float lastLayoutTime;
	protected Rect textArea;
	protected float firstTick;
	protected Vector2 startPosition;
	
	public HorizontalPatrollingLabel(string text, float speed, Vector2 startPosition, float stopPosition) : base()
	{
		this.textArea = new Rect(startPosition.x, startPosition.y, 0, 0);
		
		this.Text = text;
		this.Speed = speed;
		this.StopPosition = stopPosition;
		this.startPosition = startPosition;

		this.lastLayoutTime = 0;
		this.firstTick = 0;		
	}
	
	protected void CalculateFrame()
	{
		// Calculate the text width.
		Vector2 textSize = GetStyle().CalcSize(new GUIContent(this.Text));
		
		textArea.width = textSize.x;
		textArea.height = textSize.y;
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
		
		// We align the left border, so we compare to textArea.x
		if (this.Speed > 0)
		{
			if (this.textArea.x + this.textArea.width > this.StopPosition)
			{
				this.textArea.x = this.StopPosition - this.textArea.width;
				this.Speed = -this.Speed;
			}
		}
		else
		{
			if (this.textArea.x < this.startPosition.x)
			{
				this.textArea.x = this.startPosition.x;
				this.Speed = -this.Speed;
			}
		}
		
//		if (this.AlignedBorder == BorderAlignment.LEFT)
//		{
//			// We align the left border, so we compare to textArea.x
//			if (this.Speed > 0)
//			{
//				if (this.textArea.x > this.StopPosition)
//				{
//					this.textArea.x = this.StopPosition;
//					this.AnimationDone = true;
//				}
//			}
//			else
//			{
//				if (this.textArea.x < this.StopPosition)
//				{
//					this.textArea.x = this.StopPosition;
//					this.AnimationDone = true;
//				}
//			}
//		}
//		else
//		{
//			// We align the left border, so we compare to textArea.x + width
//			if (this.Speed > 0)
//			{
//				if (this.textArea.x + this.textArea.width > this.StopPosition)
//				{
//					this.textArea.x = this.StopPosition - this.textArea.width;
//					this.AnimationDone = true;
//				}
//			}
//			else
//			{
//				if (this.textArea.x + this.textArea.width < this.StopPosition)
//				{
//					this.textArea.x = this.StopPosition - this.textArea.width;
//					this.AnimationDone = true;
//				}
//			}
//		}
		
		this.lastLayoutTime = Time.time;
	}

	// Draw the label on screen
	protected void Draw()
	{
//		GUI.Box(textArea, "");
		GUI.Label(textArea, this.Text, GetStyle());
	}
	
	public override void Tick()
	{
		if (firstTick == 0)
		{
			firstTick = Time.time;
		}
		
		SaveSkin();
		
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
            RestoreSkin();
		}
	}
}
