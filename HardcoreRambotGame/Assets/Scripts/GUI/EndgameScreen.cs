using UnityEngine;
using System.Collections.Generic;

public class EndgameScreen : MonoBehaviour {

    public static int kills = 0;
    public static int hearts = 0;
    public static int score = 0;

	protected GameObject player = null;
	public GUISkin skin;
	
	protected List<AnimatedGUIControl> statsCaptions;
	protected List<AnimatedGUIControl> statsValues;
	
	// The x position at which the first stats line will stop
	public float leftStatsColumnPosition = 200;
	public float leftStatsSpeed = 1000;
	
	// Initial display delay of the first stats line
	public float initialStatsDelay = 1;
	
	// Initial distance to top of screen of the first line
	public float initialStatsOffset = 100;
	
	// Time delay between displaying each stat line
	public float nextStatsLineDelay = 0.2f;

	// Distance between stats lines
	public float nextStatsLineOffset = 70;
	
	// Initial distance to top of screen of the main caption
	public float mainCaptionOffset = 10;
	
	// Number of heartbeats for stats values
	public int statValueBeatCount = 3;

	// Beat size factor
	public int statValueBeatMaxSizeFactor = 2;

	// Duration of a single heartbeat
	public float statsValueBeatDuration = 0.25f;

	protected bool statsSetupDone = false;
	
	const HorizontalMovingLabel.BorderAlignment leftColumnAlignment = HorizontalMovingLabel.BorderAlignment.RIGHT;
	
	// Use this for initialization
	void Start () 
	{
		Reset ();
	}
	
	void Reset() 
	{
		statsCaptions = new List<AnimatedGUIControl>(16);
		statsValues = new List<AnimatedGUIControl>(16);
		
		statsSetupDone = false;
	}
	
	void SetupStatCaptions() 
	{
		Debug.Log ("EndgameScreen::SetupControls");
		
		HorizontalMovingLabel ctrl;

		float delay = initialStatsDelay;
		float controlPosition;
		
		//
		//
		//
		
		controlPosition = initialStatsOffset;
		ctrl = new HorizontalMovingLabel("Time survived:", 
										leftStatsSpeed, 
										new Vector2(0, controlPosition), 
										leftStatsColumnPosition, 
										leftColumnAlignment, 
										delay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsCaptions.Add(ctrl);

		//
		//
		//
		
		controlPosition += nextStatsLineOffset;
		delay += nextStatsLineDelay;
		ctrl = new HorizontalMovingLabel("Bots killed:", 
										leftStatsSpeed, 
										new Vector2(0, controlPosition), 
										leftStatsColumnPosition, 
										leftColumnAlignment, 
										delay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsCaptions.Add(ctrl);

		//
		//
		//
		
		controlPosition += nextStatsLineOffset;
		delay += nextStatsLineDelay;
		ctrl = new HorizontalMovingLabel("Hearts collected:", 
										leftStatsSpeed, 
										new Vector2(0, controlPosition), 
										leftStatsColumnPosition, 
										leftColumnAlignment, 
										delay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsCaptions.Add(ctrl);

		//
		//
		//
		
		controlPosition += nextStatsLineOffset;
		delay += nextStatsLineDelay;
		ctrl = new HorizontalMovingLabel("Score:", 
										leftStatsSpeed, 
										new Vector2(0, controlPosition), 
										leftStatsColumnPosition, 
										leftColumnAlignment, 
										delay);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsCaptions.Add(ctrl);
	}

	void SetupStatValues() 
	{
		Debug.Log ("EndgameScreen::SetupStatValues");
		
		HeartbeatLabel ctrl;

		float delay = initialStatsDelay;
		float controlPosition;
		
		//
		//
		//
		
		controlPosition = initialStatsOffset;
		delay += nextStatsLineDelay;
		ctrl = new HeartbeatLabel(Time.timeSinceLevelLoad+" s", 
									new Vector2(Screen.width / 2, controlPosition), 
									this.statsValueBeatDuration, 
									this.statValueBeatCount, 
									this.statValueBeatMaxSizeFactor, 
									0.2f);

		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsValues.Add(ctrl);
		
		//
		//
		//
		
		controlPosition += nextStatsLineOffset;
		delay += nextStatsLineDelay;
		ctrl = new HeartbeatLabel(kills+"",
									new Vector2(Screen.width / 2, controlPosition), 
									this.statsValueBeatDuration, 
									this.statValueBeatCount, 
									this.statValueBeatMaxSizeFactor, 
									0);

		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsValues.Add(ctrl);
		
		//
		//
		//
		
		controlPosition += nextStatsLineOffset;
		delay += nextStatsLineDelay;
		ctrl = new HeartbeatLabel(hearts+"", 
									new Vector2(Screen.width / 2, controlPosition), 
									this.statsValueBeatDuration, 
									this.statValueBeatCount, 
									this.statValueBeatMaxSizeFactor, 
									0);

		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsValues.Add(ctrl);
		
		//
		//
		//

        EndgameScreen.score += Random.Range(0, 1000);
		
		controlPosition += nextStatsLineOffset;
		delay += nextStatsLineDelay;
		ctrl = new HeartbeatLabel(score+"", 
									new Vector2(Screen.width / 2, controlPosition), 
									this.statsValueBeatDuration, 
									this.statValueBeatCount, 
									this.statValueBeatMaxSizeFactor, 
									0);

		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "EndgameScreenStatCaption";
		statsValues.Add(ctrl);
		
		statsSetupDone = true;
	}

	protected void DrawControls()
	{
		bool captionAnimationsDone = true;
		
		foreach (AnimatedGUIControl ctrl in statsCaptions)
		{
			ctrl.Tick();

			if (!ctrl.AnimationDone)
			{
				captionAnimationsDone = false;
			}
		}
		
		if (captionAnimationsDone)
		{
			foreach (AnimatedGUIControl ctrl in statsValues)
			{
				ctrl.Tick();
				
				if (!ctrl.AnimationDone)
				{
					break;
				}
			}
		}
	}
	
	protected void DrawCaption()
	{
		GUIStyle style;
		
		style = GUIUtils.FindStyleOrDefault("EndgameScreenMainCaption", GUI.skin.label);
		GUI.Label(new Rect(0, mainCaptionOffset, Screen.width, Screen.height), "HeartCore Rambot is DEAD", style);

		style = GUIUtils.FindStyleOrDefault("EndgameScreenSubCaption", GUI.skin.label);
		GUI.Label(new Rect(0, mainCaptionOffset+40, Screen.width, Screen.height), "(and it's your fault!)", style);
	}

	public void OnGUI()
    {
        if (!enabled)
            return;
	
		if (!statsSetupDone)
		{
			player = GameObject.FindWithTag("Player");
			SetupStatCaptions();
			SetupStatValues();
		}
	
		GUISkin oldSkin = GUI.skin;
		GUI.skin = this.skin;
		try
		{
			
			GUI.Box (new Rect(0,0,Screen.width,Screen.height), "");
			
			if (Event.current.isKey || Event.current.isMouse)
			{
				Application.LoadLevel("StartMenu");
			}
			
			DrawCaption();
			DrawControls();
		}
		finally
		{
			GUI.skin = oldSkin;
		}
    }
}
