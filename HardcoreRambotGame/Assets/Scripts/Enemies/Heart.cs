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

    //void OnTriggerEnter(Collider other)
    void OnCollisionEnter(Collision collisionInfo)
    {
        print("herzhafte Colision");

        Affiliation player = collisionInfo.collider.GetComponent<Affiliation>();
        if (!player)
            return;

        if (player.GetType() == typeof(Player))
        {
            Player.addHeartPower(heartPower);

            Destroyable d = GetComponent<Destroyable>();
            if (d)
                d.Die();
            else
                Destroy(this);
        }




    }
}
