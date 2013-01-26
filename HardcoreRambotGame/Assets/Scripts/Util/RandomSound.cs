using UnityEngine;
using System.Collections;

public class RandomSound : MonoBehaviour
{


    public AudioClip[] AudioSources;
	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        SetRandomSound();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetRandomSound()
    {
        var idx = Random.Range(0,AudioSources.Length-1);

        audio.clip = AudioSources[idx];
        
        audio.Play();
    }
}
