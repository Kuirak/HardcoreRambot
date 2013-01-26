using UnityEngine;
using System.Collections;

public class RandomSound : MonoBehaviour
{


    public AudioClip[] AudioSources;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Play()
    {
        var idx = Random.Range(0,AudioSources.Length-1);

        audio.clip = AudioSources[idx];

        audio.pitch = Random.Range(0.9f, 1.1f);

        audio.Play();
    }
}
