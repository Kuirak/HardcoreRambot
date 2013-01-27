using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	
	const float splashWaitTime = 5.0f;
	
	Texture splashImage;
	
	// Use this for initialization
	void Start () {
		splashImage = (Texture)Resources.Load("splashscreen2");
	}
	
    void OnGUI()

    {
        // Draw our texture on screen - full screen
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashImage, ScaleMode.StretchToFill, false, 0);

		// Call coroutine to hold splash screen up for a given amount of time
        StartCoroutine (SplashHold());

		if (Event.current.type == EventType.keyDown)
		{
			Application.LoadLevel("StartMenu");
		}
	}

    IEnumerator SplashHold()
    {
        yield return new WaitForSeconds(splashWaitTime);

        Application.LoadLevel("StartMenu");
    }
}
