using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectsManager : MonoBehaviour {

    public static SoundEffectsManager instance = null;

    // each sound effect is assigned a number in the inspector
    public AudioSource[] Sounds = new AudioSource[14];

    // Use this for initialization
    void Start()
    { }

    void Awake ()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); 
    }

    // call each of the clips by number 
    public void playSound(int clipNum)
    {
        if (clipNum < 0 || clipNum >= Sounds.Length)
            return;

        Sounds[clipNum].Play();
    }
	
	// Update is called once per frame
	void Update () {
	}
}
