using UnityEngine;
using System.Collections;

public class RambotMouseLook : MonoBehaviour
{

    public Transform RotA;

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
        transform.RotateAround(RotA.position, Vector3.up, horizontalMouseValue);
    }

    private void RotateY()
    {
        var verticalMouseValue = Input.GetAxis("Mouse Y");
        transform.RotateAround(RotA.position, transform.right, -verticalMouseValue);
        
    }
}
