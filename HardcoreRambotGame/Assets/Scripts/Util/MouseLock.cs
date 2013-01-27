using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour
{
    public PauseMenuScreen Menu;

    
	// Use this for initialization
	void Start ()
	{
	    Screen.lockCursor = true;
	    Menu = GetComponent<PauseMenuScreen>();
	    Time.timeScale = 1;
	}

    public void Pause()
    {
        print("paused");
        Menu.enabled = true;
        Screen.lockCursor = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        print("resume");
        Time.timeScale = 1;
        Screen.lockCursor = true;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!Menu.enabled)
            {
                Pause();
            }
        }

        if (Menu.enabled)
        {
            Screen.lockCursor = false;
        }
        else if (!Screen.lockCursor)
        {
            Screen.lockCursor = true;
        }
        
	}


    //void OnApplicationFocus(bool focus)
    //{
    //    if (!focus)
    //    {
    //        Pause();
    //    }
    //}
}
