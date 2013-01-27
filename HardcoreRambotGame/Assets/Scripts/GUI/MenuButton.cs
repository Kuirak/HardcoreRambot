using UnityEngine;
using System.Collections;

public class MenuButton : SkinnedGUIControl {
	
	public string Text { get; set; }
	public bool Highlighted { get; set; }
	public string HighlightedStyleName { get; set; }
	public bool Clicked { get; set; }
	public bool MouseHover { get { return IsMouseHover(); } }
	
	public Rect ButtonArea { get; set; }
	
	public MenuButton(string text, Rect buttonArea) : base()
	{
		this.Text = text;
		this.Highlighted = false;
		this.ButtonArea = buttonArea;
		
		this.HighlightedStyleName = "label";
		this.Clicked = false;
	}
	
	public void Draw()
	{
		if (this.Highlighted)
		{
			GUIStyle style = GUIUtils.FindStyleOrDefault(this.HighlightedStyleName, GUI.skin.label);
			this.Clicked = GUI.Button(this.ButtonArea, this.Text, style);
		}
		else
		{
			this.Clicked = GUI.Button(this.ButtonArea, this.Text, GetStyle());
		}
	}
	
    /// <summary>
    /// Checks if a mouse click happened inside this control
    /// be hidden.
    /// </summary>
    protected bool IsMouseHover()
    {
        // Only react to mouse up events
        UnityEngine.Event e = UnityEngine.Event.current;
		
        if (this.ButtonArea.Contains(e.mousePosition))
        {
            return true;
		}

		return false;
    }
	
	public override void Tick()
	{		
		SaveSkin();
		
		try
		{
			
//		GUI.Box(new Rect(Screen.width/2-5, 0, 10, Screen.height), "");

			Draw();
		}
		finally
		{
			RestoreSkin();
		}
	}
}
