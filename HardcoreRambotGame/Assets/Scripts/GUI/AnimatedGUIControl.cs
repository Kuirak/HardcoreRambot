using UnityEngine;
using System.Collections;

public abstract class AnimatedGUIControl {
	
	public bool AnimationDone { get; set; }
	
	public AnimatedGUIControl()
	{
		this.AnimationDone = false;
	}
	
	// Update is called once per frame
	public abstract void Tick ();
}
