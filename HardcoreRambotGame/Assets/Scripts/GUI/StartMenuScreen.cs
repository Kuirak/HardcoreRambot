using UnityEngine;
using System.Collections.Generic;

public class StartMenuScreen : MonoBehaviour {

	protected GameObject player = null;
	public GUISkin skin;
	
	protected List<SimpleLabel> labels;
	protected List<HorizontalPatrollingLabel> patrollingLabels;
	protected List<MenuButton> menuButtons;
	protected MenuButton boeppelButton;
	
	protected int selectedButtonIndex;
	
	const int MENU_INDEX_LETSROLL = 0;
	const int MENU_INDEX_CREDITS = 1;
	
	// Use this for initialization
	void Start () 
	{
		Reset ();
	}
	
	void Reset() 
	{
		labels = new List<SimpleLabel>(16);
		patrollingLabels = new List<HorizontalPatrollingLabel>(16);
		menuButtons = new List<MenuButton>(16);
		boeppelButton = null;
	}
	
	void SetupLabels() 
	{
		Debug.Log ("StartMenuScreen::SetupLabels");
		
		SimpleLabel ctrl;

		Rect textArea;
		
		//
		//
		//
		
		textArea = new Rect(0, 40, Screen.width, Screen.height);
		ctrl = new SimpleLabel("HeartCore    Rambot", textArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "StartMenuHeader";
		labels.Add(ctrl);

		//
		//
		//
		
		textArea = new Rect(0, 80, Screen.width, Screen.height);
		ctrl = new SimpleLabel("(that's you!)", textArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "StartMenuSubHeader";
		labels.Add(ctrl);

	}

	void SetupPatrollingLabels() 
	{
		Debug.Log ("StartMenuScreen::SetupPatrollingLabels");
		
		HorizontalPatrollingLabel ctrl;

		Vector2 controlPosition;
		float stopPosition;
		float speed;
		
		//
		//
		//
		
		controlPosition = new Vector2(20, 30);
		stopPosition = controlPosition.x + 150;
		speed = 100;
		ctrl = new HorizontalPatrollingLabel("A", speed, controlPosition, stopPosition);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MovingBots";
		this.patrollingLabels.Add(ctrl);

		//
		//
		//
		
		controlPosition = new Vector2(Screen.width - 300, Screen.height - 100);
		stopPosition = controlPosition.x + 200;
		speed = -50;
		ctrl = new HorizontalPatrollingLabel("B", speed, controlPosition, stopPosition);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MovingBots";
		this.patrollingLabels.Add(ctrl);
	}

	void SetupMenuButtons() 
	{
		Debug.Log ("StartMenuScreen::SetupMenuButtons");
		
		MenuButton ctrl;

		Rect buttonArea = new Rect(Screen.width/4, 200, Screen.width/2, 60);
		float spacingBetweenLines = 60;
		
		//
		//
		//
		
		ctrl = new MenuButton("Let's roll!", buttonArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MenuButton";
		ctrl.HighlightedStyleName = "MenuButtonHighlighted";
		this.menuButtons.Add(ctrl);
		
		//
		//
		//
		
		buttonArea.y += spacingBetweenLines;
		ctrl = new MenuButton("Credits", buttonArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MenuButton";
		ctrl.HighlightedStyleName = "MenuButtonHighlighted";
		this.menuButtons.Add(ctrl);
		
		// Initial selection
		selectedButtonIndex = 0;
		this.menuButtons[selectedButtonIndex].Highlighted = true;		

		// BoeppelButton
		boeppelButton = new MenuButton("f", buttonArea);
		boeppelButton.CustomSkin = this.skin;
		boeppelButton.StyleName = "MenuButtonBoeppel";
		boeppelButton.HighlightedStyleName = "MenuButtonBoeppel";
	}

	protected void DrawControls()
	{
		foreach (SimpleLabel ctrl in labels)
		{
			ctrl.Tick();
		}

		foreach (HorizontalPatrollingLabel ctrl in patrollingLabels)
		{
			ctrl.Tick();
		}

		foreach (MenuButton ctrl in menuButtons)
		{
			ctrl.Tick();
		}
		
		Rect buttonArea = this.menuButtons[selectedButtonIndex].ButtonArea;
		buttonArea.x += 60;
		this.boeppelButton.ButtonArea = buttonArea;
		this.boeppelButton.Tick();
	}
	
//	protected void DrawCaption()
//	{
//		GUIStyle style;
//		
//		style = GUIUtils.FindStyleOrDefault("EndgameScreenMainCaption", GUI.skin.label);
//		GUI.Label(new Rect(0, mainCaptionOffset, Screen.width, Screen.height), "HeartCore Rambot is DEAD", style);
//
//		style = GUIUtils.FindStyleOrDefault("EndgameScreenSubCaption", GUI.skin.label);
//		GUI.Label(new Rect(0, mainCaptionOffset+40, Screen.width, Screen.height), "(and it's your fault!)", style);
//	}
	
	protected void UpdateSelectedIndex(int offset)
	{
		menuButtons[this.selectedButtonIndex].Highlighted = false;
		
		this.selectedButtonIndex += offset;
		
		if (this.selectedButtonIndex < 0)
			this.selectedButtonIndex = 0;

		if (this.selectedButtonIndex >= menuButtons.Count)
			this.selectedButtonIndex = menuButtons.Count - 1;

		menuButtons[this.selectedButtonIndex].Highlighted = true;
	}
	
	protected void LetsRoll()
	{
		Debug.Log("Let's roll!");
	}
	
	protected void ShowCredits()
	{
		Debug.Log("Credits");
	}
	
	public void OnGUI()
    {
        if (!enabled)
			return;
		
		if (this.labels.Count == 0)
			SetupLabels();
	
		if (this.patrollingLabels.Count == 0)
			SetupPatrollingLabels();
		
		if (this.menuButtons.Count == 0)
			SetupMenuButtons();

		GUISkin oldSkin = GUI.skin;
		GUI.skin = this.skin;
		
		try
		{
			
			GUI.Box (new Rect(0,0,Screen.width,Screen.height), "");
//		GUI.Box(new Rect(Screen.width/2-5, 0, 10, Screen.height), "");

    		if (Event.current.Equals (Event.KeyboardEvent ("up")))
			{
        		UpdateSelectedIndex(-1);
			}
    		else if (Event.current.Equals (Event.KeyboardEvent ("down")))
			{
				UpdateSelectedIndex(1);
			}
			else if (
					Event.current.Equals(Event.KeyboardEvent ("return")) ||
				 	Event.current.Equals(Event.KeyboardEvent ("space"))
				)
			{
				switch (this.selectedButtonIndex)
				{
				case MENU_INDEX_LETSROLL:
					LetsRoll();
					break;
				case MENU_INDEX_CREDITS:
					ShowCredits();
					break;
				default:
					break;
				}
			}
			
			DrawControls();
		}
		finally
		{
			GUI.skin = oldSkin;
		}
    }
}
