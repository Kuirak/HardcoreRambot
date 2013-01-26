using UnityEngine;
using System.Collections;

public class Player : Affiliation
{
    public static float heartPower = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void addHeartPower(float power)
    {
        heartPower += heartPower;
    }
}
