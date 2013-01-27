using UnityEngine;
using System.Collections;

public class Player : Affiliation
{
    public float heartPower = 0;
    public int level = 1;

    public float[] levelUpHearts;

    public static Player instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addHeartPower(float power)
    {
        heartPower += power;
        if(level>7) return;
        if (heartPower >= levelUpHearts[level - 1])
        {
            Weapon w = FindObjectOfType(typeof(Weapon)) as Weapon;
            level++;

            Destroyable d = GetComponent<Destroyable>();
            d.reset();

            for (int i=0;i<w.visibleWeapon.Length;i++)
                if (w.visibleWeapon[i])
                    w.visibleWeapon[i].gameObject.SetActive(level-1 == i);
            print("Level Up!! Now: "+level);
        }
    }
}
