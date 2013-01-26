using UnityEngine;
using System.Collections;

public class SimpleLabel : SkinnedGUIControl {
	
	public string Text { get; set; }
	
	protected Rect textArea;
	
	public SimpleLabel(string text, Rect textArea) : base()
	{
		this.Text = text;
		this.textArea = textArea;
	}
	
	public void Draw()
	{
		GUI.Label(this.textArea, this.Text, GetStyle());
	}
	
	public override void Tick()
	{		
		SaveSkin();
		
		try
		{
			
			GUI.Box (new Rect(0,0,Screen.width,Screen.height), "");
//		GUI.Box(new Rect(Screen.width/2-5, 0, 10, Screen.height), "");

			Draw();
		}
		finally
		{
			RestoreSkin();
		}
	}
}
