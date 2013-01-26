using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject spawn;
    public float timeBetweenSpawns = 5;
    float lastSpawnTime = 0;

    public int minLevel = 0;
    public int maxLevel = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > lastSpawnTime + timeBetweenSpawns)
        {
            if (minLevel!=0 && Player.instance.level < minLevel)
                return;

            if (maxLevel != 0 && Player.instance.level > maxLevel)
                return;

            lastSpawnTime = Time.time;
            GameObject.Instantiate(spawn, transform.position, transform.rotation);
        }
	
	}
}
