using UnityEngine;
using System.Collections;

public abstract class AnimatedGUIControl : SkinnedGUIControl {
	
	public bool AnimationDone { get; set; }
		
	public AnimatedGUIControl() : base()
	{
		this.AnimationDone = false;
	}
}
