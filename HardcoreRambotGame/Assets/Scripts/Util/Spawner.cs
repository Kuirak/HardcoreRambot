using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject spawn;
    public float timeBetweenSpawns = 5;
    float lastSpawnTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > lastSpawnTime + timeBetweenSpawns)
        {
            lastSpawnTime = Time.time;
            GameObject.Instantiate(spawn, transform.position, transform.rotation);
        }
	
	}
}
