using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float damage;

    public float speed = 100;
    public float acc = 0;

    Destroyable destroyable;

	// Use this for initialization
	void Start () {
        destroyable = GetComponent<Destroyable>();
	}
	
	// Update is called once per frame
	void Update () {

        speed += acc * Time.deltaTime;

        if (rigidbody)
            rigidbody.MovePosition(rigidbody.position + transform.forward * speed);	
        else
            transform.Translate(transform.forward * speed);
	}

    void OnTriggerEnter(Collider other) 
    //void OnCollisionEnter(Collision collisionInfo)
    {


        Destroyable target = other.GetComponent<Destroyable>();
        if (!target)
            return;


        //print("Collision with " + destroyable.affiliation.GetType() + " and " + target.affiliation.GetType());
        
        if (destroyable.affiliation.GetType() != target.affiliation.GetType())  // No friendly fire and self-hit
        {
            target.receiveDamage(damage);

            Destroyable d = GetComponent<Destroyable>();
            if (d)
                d.Die();
            else
                Destroy(this);

            rigidbody.AddExplosionForce(100, transform.position, 100);
        }

        


    }
}
