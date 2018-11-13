using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    public float seconds = 2;
    private float curSeconds = 0;
	
	void Update () {
        curSeconds += Time.deltaTime;
        if (curSeconds > seconds)
        {
            Destroy(gameObject);
        }
	}
}
