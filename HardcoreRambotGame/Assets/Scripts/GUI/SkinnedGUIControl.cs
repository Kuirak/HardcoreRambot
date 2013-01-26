using UnityEngine;
using System.Collections;

public abstract class SkinnedGUIControl {
	
    // Custom gui skin for labels
    public GUISkin CustomSkin { get; set; }
    public string StyleName { get; set; }
	
	protected GUISkin oldSkin;
	
	public SkinnedGUIControl()
	{
		this.StyleName = "label";
		this.oldSkin = null;
	}
	
	// Return the style for this label. 
	protected GUIStyle GetStyle()
	{
		return GUIUtils.FindStyleOrDefault(this.StyleName, GUI.skin.label);
	}
	

	// Update is called once per frame
	public abstract void Tick ();
	
	protected void SaveSkin()
	{
		if (this.CustomSkin != null)
		{
			this.oldSkin = GUI.skin;
			GUI.skin = this.CustomSkin;
		}
	}
	
	protected void RestoreSkin()
	{
		if (this.oldSkin != null)
		{
			GUI.skin = this.oldSkin;
			this.oldSkin = null;
		}
	}
}
