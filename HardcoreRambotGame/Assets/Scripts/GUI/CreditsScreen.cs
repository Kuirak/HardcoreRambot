using UnityEngine;
using System.Collections.Generic;

public class CreditsScreen : MonoBehaviour {
	
	protected GameObject player = null;
	public GUISkin skin;
	
	protected List<AnimatedGUIControl> controls;
	
	// Initial distance to top of screen of the first line
	public float initialOffset = 100;

	// Distance between lines
	public float nextLineOffset = 20;

	// Distance between lines
	public float lineSpeed = 100;
	
	const HorizontalMovingLabel.BorderAlignment leftColumnAlignment = HorizontalMovingLabel.BorderAlignment.RIGHT;
	protected float enabledStartTime;
	
	// Use this for initialization
	void Start () 
	{
		Reset ();
	}
	
	void Reset() 
	{
		controls = new List<AnimatedGUIControl>(16);
		enabledStartTime = 0;
		Time.timeScale = 1;
	}
	
	void SetupControls() 
	{
		Debug.Log ("CreditsScreen::SetupControls");
		
		HorizontalMovingLabel ctrl;

		const float jobDelay = 1f;
		const float nameDelay = 0.3f;
		float controlPosition;
		float stopPosition = Screen.width / 2;
		float currentDelay = 0;
		HorizontalMovingLabel.BorderAlignment jobAlignment = HorizontalMovingLabel.BorderAlignment.RIGHT;
		HorizontalMovingLabel.BorderAlignment nameAlignment = HorizontalMovingLabel.BorderAlignment.LEFT;
		
		//
		//
		//
		
		currentDelay += jobDelay;
		controlPosition = initialOffset;
		ctrl = new HorizontalMovingLabel("Artwork", 
										lineSpeed, 
										new Vector2(0, controlPosition), 
										stopPosition, 
										jobAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenJobLabel";
		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Simon Abbt", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Martin Hones", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("David Seifers", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += jobDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Coding", 
										lineSpeed, 
										new Vector2(0, controlPosition), 
										stopPosition, 
										jobAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenJobLabel";
		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Adam Burg", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Jonas Kugelmann", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);
		
		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Thomas Wagner", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += jobDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Sound effects", 
										lineSpeed, 
										new Vector2(0, controlPosition), 
										stopPosition, 
										jobAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenJobLabel";
		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("David Seifers", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);

		//
		//
		//
		
		currentDelay += nameDelay;
		controlPosition += nextLineOffset;
		ctrl = new HorizontalMovingLabel("Thomas Wagner", 
										-lineSpeed, 
										new Vector2(Screen.width, controlPosition), 
										stopPosition, 
										nameAlignment, 
										currentDelay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "CreditScreenNameLabel";

		controls.Add(ctrl);
	}

	protected void DrawControls()
	{
		foreach (AnimatedGUIControl ctrl in controls)
		{
			ctrl.Tick();
		}
	}
	
	public void OnGUI()
    {
        if (!enabled)
            return;
	
		if (controls.Count == 0)
		{
			enabledStartTime = Time.time;
			SetupControls();
		}
	
		GUISkin oldSkin = GUI.skin;
		GUI.skin = this.skin;
		
		try
		{
			
			GUI.Box (new Rect(0,0,Screen.width,Screen.height), "");

			// Allow aborting the EndgameScreen after 3 seconds
			if ((Event.current.isKey || Event.current.isMouse) && enabledStartTime > 0 && Time.time > enabledStartTime + 1f) 
			{
				Application.LoadLevel("StartMenu");
			}
			
			DrawControls();
		}
		finally
		{
			GUI.skin = oldSkin;
		}
    }
}
