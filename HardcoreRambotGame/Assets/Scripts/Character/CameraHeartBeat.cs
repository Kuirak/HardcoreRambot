using UnityEngine;
using System.Collections;

public class CameraHeartBeat : MonoBehaviour {

    Player player;
    Destroyable playerDest;
    Colorize colorize;
    float maxHealth;
    public float minFreq = 60;
    public float maxFreq = 160;

    float lastBeatTime = 0;
    bool skip = false;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType(typeof(Player)) as Player;
        playerDest = player.GetComponent<Destroyable>();
        colorize = GetComponent<Colorize>();
        maxHealth = playerDest.health;
	}
	
	// Update is called once per frame
	void Update () {
        float healthPercent = playerDest.health / maxHealth;
        float freq = Mathf.Lerp(minFreq, maxFreq, 1 - healthPercent);
        

        float timeSinceLastBeat = Time.time - lastBeatTime;
        float col = 0;
        float len = 1 / (freq / 60) * 0.5f;
        if (timeSinceLastBeat < len)
            col = Mathf.Lerp(0, 1, timeSinceLastBeat / len);
        else
            if (timeSinceLastBeat < 2 * len)
                col = Mathf.Lerp(1, 0, timeSinceLastBeat / len - len);
            else col = 0;
        //print(freq);
        if (skip) col = 0;

        //Vector3 c = Mathf.Lerp(new Vector3(1, 1, 1), new Vector3(1, 0, 0), col);
        colorize.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, healthPercent, healthPercent, 1), col);
        audio.pitch = Mathf.Lerp(0.8f, 1.5f, 1-healthPercent);

        if (timeSinceLastBeat >= 1 / (freq/60))
        {
            skip = (Random.Range(1, 100) < 5);           

            lastBeatTime = Time.time;
            if (!skip)
                audio.Play();
        }
	}
}
