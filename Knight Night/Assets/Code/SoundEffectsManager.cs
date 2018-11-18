using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectsManager : MonoBehaviour {

    public static SoundEffectsManager instance = null;
    public GameObject sfxPrefab;

    // each sound effect is assigned a number in the inspector
    public AudioClip[] Sounds;

    void Awake ()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // call each of the clips by number 
    public void playSound(int clipNum, bool looping, float vol = 1.0f)
    {
        if (clipNum < 0 || clipNum >= Sounds.Length)
            return;

        GameObject g = Instantiate(sfxPrefab);
        g.GetComponent<AudioSource>().loop = looping;
        g.GetComponent<AudioSource>().volume = vol;
        g.GetComponent<AudioSource>().clip = Sounds[clipNum];
        g.GetComponent<AudioSource>().Play();
    }
}
