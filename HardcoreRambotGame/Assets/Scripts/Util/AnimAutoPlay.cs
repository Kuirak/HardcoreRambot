using UnityEngine;
using System.Collections;

public class AnimAutoPlay : MonoBehaviour {

    public string animName;

	// Use this for initialization
	void Start () {
        animation.Play(animName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
