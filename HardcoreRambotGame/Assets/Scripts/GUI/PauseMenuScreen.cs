using UnityEngine;
using System.Collections.Generic;

public class PauseMenuScreen : MonoBehaviour
{

    public MouseLock game;
	protected GameObject player = null;
	public GUISkin skin;
	
	protected List<SimpleLabel> labels;
	protected List<HorizontalPatrollingLabel> patrollingLabels;
	protected List<MenuButton> menuButtons;
	protected MenuButton boeppelButton;
	
	protected int selectedButtonIndex;
	
	const int MENU_INDEX_CONTINUE = 0;
	const int MENU_INDEX_RESTART = 1;
	const int MENU_INDEX_EXIT = 2;
	
	protected bool buttonClicked = false;
	
	// Use this for initialization
	void Start ()
	{
	    game = GetComponent<MouseLock>();
		Reset ();
	}
	
	void Reset() 
	{
		labels = new List<SimpleLabel>(16);
		patrollingLabels = new List<HorizontalPatrollingLabel>(16);
		menuButtons = new List<MenuButton>(16);
		boeppelButton = null;
		selectedButtonIndex = 0;
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
		ctrl = new SimpleLabel("Having a smoke...", textArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "StartMenuHeader";
		labels.Add(ctrl);

		//
		//
		//
		
		textArea = new Rect(0, 80, Screen.width, Screen.height);
		ctrl = new SimpleLabel("(switched off robot power, too)", textArea);
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
		
		controlPosition = new Vector2(20, Screen.height - 100);
		stopPosition = controlPosition.x + 150;
		speed = 100;
		ctrl = new HorizontalPatrollingLabel("A", speed, controlPosition, stopPosition);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MovingBots";
		this.patrollingLabels.Add(ctrl);

		//
		//
		//
		
		controlPosition = new Vector2(Screen.width - 200, 30);
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

		Rect buttonArea = new Rect(Screen.width/4, 180, Screen.width/2, 60);
		float spacingBetweenLines = 60;
		
		//
		//
		//
		
		ctrl = new MenuButton("Jump back in", buttonArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MenuButton";
		ctrl.HighlightedStyleName = "MenuButtonHighlighted";
		this.menuButtons.Add(ctrl);
		
		//
		//
		//
		
		buttonArea.y += spacingBetweenLines;
		ctrl = new MenuButton("Restart", buttonArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MenuButton";
		ctrl.HighlightedStyleName = "MenuButtonHighlighted";
		this.menuButtons.Add(ctrl);
		
		//
		//
		//
		
		buttonArea.y += spacingBetweenLines;
		ctrl = new MenuButton("Exit to menu", buttonArea);
		ctrl.CustomSkin = this.skin;
		ctrl.StyleName = "MenuButton";
		ctrl.HighlightedStyleName = "MenuButtonHighlighted";
		this.menuButtons.Add(ctrl);
		
		// Initial selection
		UpdateSelectedIndex(-selectedButtonIndex); // set to zero

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

		for (int i = 0; i < menuButtons.Count; i++)
		{
			MenuButton ctrl = menuButtons[i];
			ctrl.Tick();
			
			if (ctrl.MouseHover)
			{
				UpdateSelectedIndex(i - selectedButtonIndex);
			}
			
			if (ctrl.Clicked)
			{
				buttonClicked = true;
			}
		}
		
		Rect buttonArea = this.menuButtons[selectedButtonIndex].ButtonArea;
		buttonArea.x += 30;
		this.boeppelButton.ButtonArea = buttonArea;
		this.boeppelButton.Tick();
	}
	
	protected void UpdateSelectedIndex(int offset)
	{
		int previousIndex = this.selectedButtonIndex;
		
		menuButtons[this.selectedButtonIndex].Highlighted = false;
		
		this.selectedButtonIndex += offset;
		
		if (this.selectedButtonIndex < 0)
			this.selectedButtonIndex = 0;

		if (this.selectedButtonIndex >= menuButtons.Count)
			this.selectedButtonIndex = menuButtons.Count - 1;

		menuButtons[this.selectedButtonIndex].Highlighted = true;
		
		if (previousIndex != this.selectedButtonIndex)
		{
	        if (audio)
			{
	            audio.Play();
			}
		}
	}
	
	protected void Continue()
	{
        if (audio)
            audio.Play();
	    this.enabled = false;
	    Reset();
        
		if (!game)
		{
        	return;
		}
		
	    game.Resume();
	}
	
	protected void RestartLevel()
	{
        if (audio)
            audio.Play();
		Application.LoadLevel("Arena");
	}
	
	protected void ExitToMainMenu()
	{
        if (audio)
            audio.Play();
		Application.LoadLevel("StartMenu");
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

    		if (Event.current.Equals (Event.KeyboardEvent ("up")))
			{
        		UpdateSelectedIndex(-1);
			}
    		else if (Event.current.Equals (Event.KeyboardEvent ("down")))
			{
				UpdateSelectedIndex(1);
			}
            else if (Event.current.Equals(Event.KeyboardEvent("escape")))
    		{
    		    Continue();
    		}
    		
			else if (
					Event.current.Equals(Event.KeyboardEvent ("return")) ||
				 	Event.current.Equals(Event.KeyboardEvent ("space")) ||
					buttonClicked
				)
			{
				switch (this.selectedButtonIndex)
				{
				case MENU_INDEX_CONTINUE:
					Continue();
					return;
				case MENU_INDEX_RESTART:
					RestartLevel();
					return;
				case MENU_INDEX_EXIT:
					ExitToMainMenu();
					return;
				default:
					break;
				}
			}
			
			GUI.Box (new Rect(0, 0, Screen.width, Screen.height), "");
			DrawControls();
		}
		finally
		{
			GUI.skin = oldSkin;
		}
    }
}
