using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
    public Transform Spawn;
    public Transform Target;
    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start ()
	{
	    lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    lineRenderer.SetPosition(0,Spawn.position);
        lineRenderer.SetPosition(1,Target.position);
	}
}
