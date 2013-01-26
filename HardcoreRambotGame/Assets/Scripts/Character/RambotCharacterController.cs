using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RambotCharacterController : MonoBehaviour
{
    private Vector3 Speed = Vector3.zero;
    public float ForwardSpeed = 1;
    public float StrafeSpeed = 1;
    public float JumpStrength = 1;
    public Transform Body;
    public float GravityStrength=0.1f;
    private CharacterController _controller;
    

	// Use this for initialization
	void Start ()
	{
	    _controller = GetComponent<CharacterController>();
	    Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        var moveDir = Strafe();
        moveDir += Forward();
        if(_controller.isGrounded)
        {
            Speed = Vector3.zero;
        }
	    Speed += Jump();
	    Speed += Physics.gravity * GravityStrength;
	    moveDir += Speed;
	    moveDir *= Time.fixedDeltaTime;
	    _controller.Move(moveDir);
	    
	}


    private Vector3 Strafe()
    {
        var horizontalValue = Input.GetAxis("Horizontal");
        return Body.right * horizontalValue * StrafeSpeed;
    }

    private Vector3 Forward()
    {
        var verticalValue = Input.GetAxis("Vertical");
        return Body.forward * verticalValue * ForwardSpeed;
    }



    private Vector3 Jump()
    {
        var jump = Input.GetButtonDown("Jump");
        
        if (jump && _controller.isGrounded )
        {
            
            Debug.Log("Jump");
            return Vector3.up*JumpStrength;
        }
        return Vector3.zero;
    }

   
}
