using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectsManager : MonoBehaviour {

    public static SoundEffectsManager instance = null;
    
    public AudioSource buttonDeselect;
    public AudioSource buttonDown;
    public AudioSource buttonSelect;
    public AudioSource buttonUp;
    public AudioSource cheerAmbient;
    public AudioSource cheerExcite;
    public AudioSource gallop;
    public AudioSource lanceHitGround;
    public AudioSource lanceHitKnight;
    public AudioSource recordHitGround;
    public AudioSource recordHitKnight;
    public AudioSource rocketExplode;
    public AudioSource rocketShoot;
    public AudioSource throwWeapon;
    public AudioSource[] Sounds = new AudioSource[14];
   
    // Use this for initialization
    void Start ()
    {
        // assign each sound effect a number 
        Sounds[0] = buttonDeselect;
        Sounds[1] = buttonDown;
        Sounds[2] = buttonSelect;
        Sounds[3] = buttonUp;
        Sounds[4] = cheerAmbient;
        Sounds[5] = cheerExcite;
        Sounds[6] = gallop;
        Sounds[7] = lanceHitGround;
        Sounds[8] = lanceHitKnight;
        Sounds[9] = recordHitGround;
        Sounds[10] = recordHitKnight;
        Sounds[11] = rocketExplode;
        Sounds[12] = rocketShoot;
        Sounds[13] = throwWeapon; 
	}

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
