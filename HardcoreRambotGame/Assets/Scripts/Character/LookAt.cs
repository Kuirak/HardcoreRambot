using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{

    public Transform HorizontalLookAt;
    public Transform VerticalLookAt;
    public Transform Target;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var hVec = new Vector3(Target.position.x, HorizontalLookAt.position.y, Target.position.z);
        HorizontalLookAt.LookAt(hVec,Vector3.up);
	    VerticalLookAt.LookAt(Target.position,Vector3.up);
	}
}
