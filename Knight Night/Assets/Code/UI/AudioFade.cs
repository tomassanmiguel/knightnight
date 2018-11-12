using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour {

    public float fadeTime;

    private AudioSource audio;
    private float initVol;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    
    void Start () {
        initVol = audio.volume;
        audio.volume = 0;
        StartCoroutine(FadeInMusic());
	}
	
    private IEnumerator FadeInMusic()
    {
        float idx = 0f;
        float increment = 1 / (fadeTime / 0.01f);
        while (audio.volume < initVol)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            audio.volume = Mathf.Lerp(0, initVol, idx);
            idx += increment;
        }
    }
}
