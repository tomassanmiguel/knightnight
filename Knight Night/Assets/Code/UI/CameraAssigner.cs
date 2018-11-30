using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAssigner : MonoBehaviour {

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        if(canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
            this.enabled = false;
        }
    }


}
