using UnityEngine;
using System.Collections;

public class Targeting : MonoBehaviour
{

    public Camera Cam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var y = Screen.height/2;
	    var x = Screen.width/2;
	    var pos = Cam.ScreenToWorldPoint(new Vector3(x, y, 100));
	    transform.position = pos;
	}
}
