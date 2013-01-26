using UnityEngine;
using System.Collections;

public class RambotMouseLook : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    RotateX();
        RotateY();
	}

    private void RotateX()
    {
        var horizontalMouseValue = Input.GetAxis("Mouse X");
        transform.RotateAround(transform.parent.position,Vector3.up, horizontalMouseValue);
    }

    private void RotateY()
    {
        var verticalMouseValue = Input.GetAxis("Mouse Y");
        transform.RotateAround(transform.parent.position,transform.right, -verticalMouseValue);
        
    }
}
