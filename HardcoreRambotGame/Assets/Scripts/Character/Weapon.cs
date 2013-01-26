using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Transform[] bullet;
    public float[] shootsPerMinute;
    public GameObject[] visibleWeapon;
    public float JitterStrength = 0.01f;
	public GameObject muzzleFlash;

    float lastShotTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
			//Instantiate(muzzleFlash, transform.position, transform.rotation);
            Shoot();
    }

    public void Shoot() 
    {
        int lvl = Player.instance.level;

        if (Time.time > lastShotTime + 1 / shootsPerMinute[lvl-1])
        {
            
            lastShotTime = Time.time;
            Instantiate(bullet[lvl - 1], transform.position, Quaternion.Lerp(transform.rotation,Random.rotation,JitterStrength) );
        }
	}
}
