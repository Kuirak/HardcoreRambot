using UnityEngine;
using System.Collections;

public class HeartbeatLabel : AnimatedGUIControl {

	// The text to display
	public string Text { get; set; }
	
	// Duration of a full hearbeat in seconds
	public float BeatDuration { get; set; }

	// Number of beats
	public int NumberOfBeats { get; set; }
	
	// Maximum factor by which the label is enlarged
	public float MaxSizeFactor { get; set; }
	
	//
	// Private
	//
	
	protected float lastLayoutTime;
	protected Rect textArea;

	protected float firstTick;
	protected float beatStart;
	
	// Time after first call to Tick() when to start displaying the labels.
	protected float displayAfter;
	protected GUIStyle style;
	protected float originalFontSize;
	
	public HeartbeatLabel(string text, Vector2 position, float beatDuration, int numberOfBeats, float maxSizeFactor, float displayAfter) : base()
	{
		this.textArea = new Rect(position.x, position.y, 0, 0);
		
		this.Text = text;
		this.BeatDuration = beatDuration;
		this.MaxSizeFactor = maxSizeFactor;
		this.NumberOfBeats = numberOfBeats;
		this.displayAfter = displayAfter;

		this.lastLayoutTime = 0;
		this.firstTick = 0;
		this.beatStart = -1;
	}
	
	
	// Draw the label on screen
	protected void Draw()
	{
		if (beatStart < 0)
		{
			this.beatStart = Time.time;
			
			this.style = new GUIStyle(GetStyle());
			this.originalFontSize = this.style.fontSize;
		}
		
		if (this.beatStart + (this.BeatDuration * this.NumberOfBeats) > Time.time)
		{
			float elapsed = Time.time - this.beatStart;
			float t = Mathf.Abs(Mathf.Sin(Mathf.PI * elapsed / this.BeatDuration));
			float factor =  Mathf.Lerp(1, this.MaxSizeFactor, t);
//			float factor =  Mathf.Lerp(1, this.MaxSizeFactor, Mathf.Abs(Mathf.Sin(elapsed/this.BeatDuration)));
			
			this.style.fontSize = (int)(originalFontSize * factor);

			// Beats not done yet
		}
		else
		{
			this.AnimationDone = true;
		}
		
		GUI.Label(textArea, this.Text, this.style);
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
			Draw();
		}
		finally
		{
            GUI.skin = oldSkin;
		}
	}
}
