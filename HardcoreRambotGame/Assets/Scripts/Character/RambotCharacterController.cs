using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RambotCharacterController : MonoBehaviour
{

    public float ForwardSpeed = 10;
    public float StrafeSpeed = 10;
    public float JumpStrength = 10;
    

    private CharacterController _controller;


	// Use this for initialization
	void Start ()
	{
	    _controller = GetComponent<CharacterController>();
	    Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
        var moveDir = Strafe();
        moveDir += Forward();
        Rotate();
        moveDir += Jump();
        _controller.SimpleMove(moveDir);
	}


    private Vector3 Strafe()
    {
        var horizontalValue = Input.GetAxis("Horizontal");
        return transform.right*horizontalValue*StrafeSpeed;
    }

    private Vector3 Forward()
    {
        var verticalValue = Input.GetAxis("Vertical");
        return transform.forward*verticalValue*ForwardSpeed;
    }

    private void Rotate()
    {
        var horizontalMousevalue = Input.GetAxis("Mouse X");
        transform.Rotate(transform.up,horizontalMousevalue);
    }

    private Vector3 Jump()
    {
        var jump = Input.GetButtonDown("Jump");
        
        if (jump && _controller.isGrounded)
        {
            Debug.Log("Jump");
            return transform.up*JumpStrength;
        }
        return Vector3.zero;
    }

   
}
