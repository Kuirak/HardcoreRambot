using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class Jetpack : MonoBehaviour
{
    public float Fuel = 100;
    public float MaxFuel = 100;
    public float BurnRate = 10;
    public float RefillRate = 8;
    public float JetpackStrength = 100;
    public float GravityStrength = 0.1f;

    public float MaxEmissionRate = 50;
    public float EmissionAfterBurn = 50;
  

    private CharacterController _controller;
    private ParticleSystem[] jetpackstreams;
	// Use this for initialization
	void Start ()
	{
	    _controller = GetComponent<CharacterController>();
	    var jetpack = transform.Find("Body").FindChild("Jetpack");
	    jetpackstreams = jetpack.GetComponentsInChildren<ParticleSystem>();
        Debug.Log(jetpackstreams[0] +""+ jetpackstreams[1]);
	}
	
	// Update is called once per frame
	void Update ()
	{
        var jump = Input.GetButton("Jump");
        if(Fuel+RefillRate*Time.deltaTime<MaxFuel)
        {
            Fuel += RefillRate*Time.deltaTime;
        }
	    foreach (var jetpackstream in jetpackstreams)
	    {
	        if(jetpackstream.emissionRate>0)
	        {
	            jetpackstream.emissionRate -= EmissionAfterBurn*Time.deltaTime;
	        }
	    }
	    if (!jump) return;
	    if(Fuel> BurnRate*Time.deltaTime)
	    {
	        Fuel -= BurnRate*Time.deltaTime;
	        var moveDir = Vector3.up*JetpackStrength;
            moveDir += Physics.gravity * GravityStrength;
	        moveDir *= Time.deltaTime;
	        _controller.Move(moveDir);
	        foreach (var jetpackstream in jetpackstreams)
	        {
	            jetpackstream.emissionRate = MaxEmissionRate;
	        }
	    }


	}
}
