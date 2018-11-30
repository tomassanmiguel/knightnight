using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTimedSound : MonoBehaviour {

    [SerializeField] private GameObject sfxPrefab;

    // each sound effect is assigned a number in the inspector
    [SerializeField] private AudioClip[] Sounds;

    public void PlaySound(int clipNum)
    {
        if (clipNum < 0 || clipNum >= Sounds.Length)
            return;

        GameObject g = Instantiate(sfxPrefab);
        g.GetComponent<AudioSource>().loop = false;
        g.GetComponent<AudioSource>().volume = 1;
        g.GetComponent<AudioSource>().clip = Sounds[clipNum];
        g.GetComponent<AudioSource>().Play();
    }
}
