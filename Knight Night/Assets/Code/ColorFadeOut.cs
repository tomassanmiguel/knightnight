using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFadeOut : MonoBehaviour {
    [SerializeField]
    private float rate;
    private SpriteRenderer spr;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        float rVal = Random.value;
        float gVal = Random.value;
        float bVal = Random.value;
        spr.color = new Color(rVal, gVal, bVal);
    }
	void Update () {
        Color c = spr.color;
        c.a -= rate * Time.deltaTime;
        spr.color = c;
	}
}
