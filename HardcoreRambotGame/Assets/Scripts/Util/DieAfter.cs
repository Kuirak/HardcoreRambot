using UnityEngine;
using System.Collections;

public class DieAfter : MonoBehaviour {

    public float dieAfter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (dieAfter > 0)
        {
            dieAfter -= Time.deltaTime;
            if (dieAfter <= 0 && gameObject !=null)
                Destroy(gameObject);
        }
	
	}
}
