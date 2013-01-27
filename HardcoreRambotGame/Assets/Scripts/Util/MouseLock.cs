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
            ShowMenu = !ShowMenu;
	        
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
        if (!focus) return;
        Screen.lockCursor = false;
        ShowMenu = true;
    }
}
