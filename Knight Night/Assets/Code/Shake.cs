﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    float shakeDuration;
    float shakeIntensity;
    bool shaking;
    Vector3 originalPosition;

    void Awake()
    {
        shaking = false;
    }
	void Update ()
    {
        if (shaking)
        {
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
        }
	}

    public void startShake(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;

        //Limitation- cannot execute multiple concurrent shakes
        if (!shaking)
        {
            StartCoroutine("ShakeNow");
        }
    }

    IEnumerator ShakeNow()
    {
        originalPosition = transform.position;
        shaking = true;
        yield return new WaitForSeconds(shakeDuration);
        shaking = false;
        transform.position = originalPosition;
    }
}