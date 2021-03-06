using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Transform[] bullet;
    public float[] shootsPerMinute;
    public GameObject[] visibleWeapon;
    public float JitterStrength = 0.01f;
	

    float lastShotTime = 0;
    public ParticleSystem Particle;
	// Use this for initialization
	void Start ()
	{
	    Particle =GetComponent<ParticleSystem>();
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
            Quaternion r = Quaternion.Lerp(transform.rotation, Random.rotation, JitterStrength);
            Instantiate(bullet[lvl - 1], transform.position, r );
            Particle.Play();
           
        }
	}
}
