using UnityEngine;
using System.Collections;

public class MenuButton : SkinnedGUIControl {
	
	public string Text { get; set; }
	public bool Highlighted { get; set; }
	public string HighlightedStyleName { get; set; }
	
	public Rect ButtonArea { get; set; }
	
	public MenuButton(string text, Rect buttonArea) : base()
	{
		this.Text = text;
		this.Highlighted = false;
		this.ButtonArea = buttonArea;
		
		this.HighlightedStyleName = "label";
	}
	
	public void Draw()
	{
		if (this.Highlighted)
		{
			GUIStyle style = GUIUtils.FindStyleOrDefault(this.HighlightedStyleName, GUI.skin.label);
			GUI.Box(this.ButtonArea, this.Text, style);
		}
		else
		{
			GUI.Box(this.ButtonArea, this.Text, GetStyle());
		}
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
