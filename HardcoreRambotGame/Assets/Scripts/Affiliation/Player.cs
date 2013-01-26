using UnityEngine;
using System.Collections;

public class Player : Affiliation
{
    public float heartPower = 0;

    public static Player instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addHeartPower(float power)
    {
        heartPower += heartPower;
    }
}
