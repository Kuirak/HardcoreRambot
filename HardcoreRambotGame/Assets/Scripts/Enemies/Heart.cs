using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {

    public float heartPower = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    //void OnCollisionEnter(Collision collisionInfo)
    {
        //Collider other = collisionInfo.collider;
        Transform parent = other.transform.parent;
        if (!parent)
            return;

        Affiliation player = parent.GetComponent<Affiliation>();
        if (!player)
            return;


        if (player.GetType() == typeof(Player))
        {
            //print("herzhafte Colision " + other.name);
            Player.instance.addHeartPower(heartPower);
            EndgameScreen.hearts++;

            Destroyable d = GetComponent<Destroyable>();
            if (d)
                d.Die();
            else
            {
                Destroy(gameObject);
                if (transform.parent)
                    Destroy(transform.parent.gameObject);
            }
        }




    }
}
