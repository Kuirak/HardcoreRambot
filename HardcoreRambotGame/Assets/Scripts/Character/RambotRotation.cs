using UnityEngine;
using System.Collections;

public class RambotRotation : MonoBehaviour
{

    public Transform Weapon;
    public float TurnSpeed = 8;
	// Use this for initialization
	void Start () 
    {
	 
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Weapon.rotation, TurnSpeed * Time.deltaTime);
	    var forward = transform.forward;
	    forward.y = 0;
	    transform.rotation = Quaternion.LookRotation(forward.normalized);
    }
}
