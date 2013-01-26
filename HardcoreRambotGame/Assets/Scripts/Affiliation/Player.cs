using UnityEngine;
using System.Collections;

public class Player : Affiliation
{
    public float heartPower = 0;
    public int level = 1;

    public float[] levelUpHearts;

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

        if (heartPower >= levelUpHearts[level - 1])
        {
            level++;
            print("Level Up!! Now: "+level);
        }
    }
}
