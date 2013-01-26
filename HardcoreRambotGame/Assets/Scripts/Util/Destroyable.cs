using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

    public float health = 100;
    public GameObject deathPrefab;
    public Affiliation affiliation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void receiveDamage(float damage)
    {
        
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        health = 0;
        if (deathPrefab)
            GameObject.Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
