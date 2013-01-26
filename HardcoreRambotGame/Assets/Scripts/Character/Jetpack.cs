using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class Jetpack : MonoBehaviour
{
    public float Fuel = 100;
    public float MaxFuel = 100;
    public float BurnRate = 10;
    public float RefillRate = 8;

    private CharacterController _controller;
    private Transform[] jetpackstreams;
	// Use this for initialization
	void Start ()
	{
	    _controller = GetComponent<CharacterController>();
	    var jetpack = transform.FindChild("Jetpack");
        jetpackstreams = jetpack.
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var jump = Input.GetKeyDown("Jump");
        if(Fuel+RefillRate*Time.deltaTime<MaxFuel)
        {
            Fuel += RefillRate*Time.deltaTime;
        }
	    if (!jump) return;
	    if(Fuel> BurnRate*Time.deltaTime)
	    {
	        Fuel -= BurnRate*Time.deltaTime;

	    }

	}
}
