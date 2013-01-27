using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour
{


    public bool ShowMenu;
	// Use this for initialization
	void Start ()
	{
	    Screen.lockCursor = true;
	    ShowMenu = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
	    {
            if(ShowMenu)
            {
                ShowMenu = false;
                Time.timeScale = 1;
            }
            else
            {
                ShowMenu = true;
                Time.timeScale = 0;
            }
	        
	    }
	    if(ShowMenu)
	    {
	        Screen.lockCursor = false;
	    }
	    else if (!Screen.lockCursor)
	    {
	        Screen.lockCursor = true;
	    }


	    

        
	}

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Screen.lockCursor = false;
            ShowMenu = true;
        }
        else
        {
            Screen.lockCursor = true;

        }
    }
}
