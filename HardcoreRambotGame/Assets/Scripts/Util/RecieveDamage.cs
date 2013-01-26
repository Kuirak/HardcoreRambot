using UnityEngine;
using System.Collections;

public class RecieveDamage : MonoBehaviour
{

    private Destroyable _destroyable;
    private const float KILL_BULLET = 10;


	// Use this for initialization
	void Start ()
	{
	    _destroyable = transform.parent.GetComponent<Destroyable>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
	    var bullet = other.GetComponent<Bullet>();
        if(!bullet)return;
        _destroyable.receiveDamage(bullet.damage);
        bullet.GetComponent<Destroyable>().receiveDamage(KILL_BULLET);

	}


}
