using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Bullet bullet;
    public float shootsPerMinute = 100;

    float lastShotTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
            Shoot();
    }

    public void Shoot() 
    {

        if (Time.time > lastShotTime + 1 / shootsPerMinute)
        {
            lastShotTime = Time.time;
            GameObject.Instantiate(bullet, transform.position, transform.rotation);
        }
	}
}
