using UnityEngine;
using System.Collections;

public class RambotCharacterController : MonoBehaviour
{

    public float ForwardSpeed = 2;
    public float StrafeSpeed = 2;

    private bool CanJump = true;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	   Strafe();
       Forward();
        
	}


    private void Strafe()
    {
        var horizontalValue = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalValue * StrafeSpeed);
    }

    private void Forward()
    {
        var verticalValue = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalValue * ForwardSpeed);
    }
}
