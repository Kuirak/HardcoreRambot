using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Bullet[] bullet;
    public float[] shootsPerMinute;

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
        int lvl = Player.instance.level;

        if (Time.time > lastShotTime + 1 / shootsPerMinute[lvl-1])
        {
            lastShotTime = Time.time;
            GameObject.Instantiate(bullet[lvl - 1], transform.position, transform.rotation);
        }
	}
}
