using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Jetpack))]
public class RambotCharacterController : MonoBehaviour
{
    private Vector3 Speed = Vector3.zero;
    public float ForwardSpeed = 1;
    public float StrafeSpeed = 1;
    public float JumpStrength = 1;
    public Transform Body;
    public float GravityStrength=0.1f;

    public Animation Anim;
    private CharacterController _controller;
    private Jetpack _jetpack;

	// Use this for initialization
	void Start ()
	{
	    _controller = GetComponent<CharacterController>();
	    _jetpack = GetComponent<Jetpack>();
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
        var moveDir = Strafe();
        moveDir += Forward();
        if(_controller.isGrounded)
        {
            Speed = Vector3.zero;
        }
	    Speed += Jump();
        if (!_jetpack.IsFlying || !Input.GetButton("Jump"))
        {
            Speed += Physics.gravity*GravityStrength;
        }
        if(_jetpack.IsFlying && _controller.velocity.y <0)
        {
            Speed = new Vector3(Speed.x,0,Speed.z);
        }
	    moveDir += Speed;
	    moveDir *= Time.deltaTime;
        if(moveDir.normalized.x == 0 && moveDir.normalized.z ==0)
        {
            Anim.CrossFade("Idle");
        }
	    _controller.Move(moveDir);
	    
	}


    private Vector3 Strafe()
    {
        var horizontalValue = Input.GetAxis("Horizontal");
        if (horizontalValue > 0)
        {
            Anim.CrossFade("walk_left");
        }
        else if (horizontalValue < 0)
        {
            Anim.CrossFade("walk_right");
        }
        
        
        return Body.right * horizontalValue * StrafeSpeed;
    }

    private Vector3 Forward()
    {
        var verticalValue = Input.GetAxis("Vertical");
        if (verticalValue > 0)
        {
            Anim.CrossFade("walk_forward");
        }
        else if (verticalValue < 0)
        {
            Anim.CrossFade("walk_backward");
        }
        
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
