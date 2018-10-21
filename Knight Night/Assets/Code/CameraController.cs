using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    public bool isSlowMo = false;
    public bool isShaking = false;
    public int shakeFactor;
    public float shakeDuration; 

    public Vector3 newCameraPos;
    public float zoomDuration;
    public float slowFactor;
    public float cameraZoom; 

    [SerializeField]
    public GameObject knight1;
    [SerializeField]
    public GameObject knight2;
    [SerializeField]
    public Camera camera;
    public Vector3 cameraPos;
    public float cameraSize; 

    // Use this for initialization
    void Start()
    {
        cameraPos = camera.transform.position;
        cameraSize = camera.orthographicSize; 
    }

    public void StartShake(int factor, float duration)
    {
        // placeholder event to trigger startShake 
        if (Input.GetKeyDown(KeyCode.T))
        {
            isShaking = true; 
            shakeFactor = factor;
            shakeDuration = duration;
        }
    }

    public void EndSlowMo()
    {
        zoomDuration = 0; 
        // camera.orthographicSize = cameraSize;
        // camera.transform.position = cameraPos;
        isSlowMo = false;
        Time.timeScale = 1.0f;
    }

    public void StartSlowMo(float factor, int playerNum)
    {
        if (isSlowMo && zoomDuration == 0)
            EndSlowMo();
        // placeholder event to trigger slowMo 
        if (Input.GetKeyDown(KeyCode.S))
        {
                slowFactor = factor;
                cameraZoom = camera.orthographicSize / 2;
                zoomDuration = 0.5f;
                isSlowMo = true;
                Time.timeScale = slowFactor;

                Vector3 playerPos;
                if (playerNum == 1)
                    playerPos = knight1.transform.position;
                else
                    playerPos = knight2.transform.position;

                // camera positioning - make sure to not go offscreen
                if (playerPos.x > 14f)
                    newCameraPos = new Vector3(14f, -3.0f, cameraPos.z);
                else if (playerPos.x < -14f)
                    newCameraPos = new Vector3(-14f, -3.0f, cameraPos.z);
                else
                    newCameraPos = new Vector3(playerPos.x, -3.0f, cameraPos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // slowMo update - zoom in 
        if (zoomDuration > 0 && isSlowMo)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, newCameraPos, Time.deltaTime / slowFactor * 8.0f);
            if (Vector3.Equals(camera.transform.position, newCameraPos))
                EndSlowMo();
            if (zoomDuration - Time.deltaTime < 0)
                zoomDuration = 0; 
            else
                zoomDuration -= Time.deltaTime; 
        }
        if (camera.orthographicSize > cameraZoom && isSlowMo)
        {
            camera.orthographicSize -= 1.0f;
        }

        // end slowMo update - zoom out 
        if (zoomDuration == 0 && !isSlowMo)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraPos, Time.deltaTime / slowFactor * 1.75f);
        }
        if (zoomDuration == 0 && !isSlowMo && camera.orthographicSize != cameraSize)
        {
            if (cameraSize - camera.orthographicSize < .75f)
                camera.orthographicSize = cameraSize;
            else
                camera.orthographicSize += .75f;
        }
   
        if (shakeDuration > 0 && isShaking)
        {
            // zoom in, otherwise camera will go offscreen 
            camera.orthographicSize = cameraSize - 1; 
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition,
              cameraPos + UnityEngine.Random.insideUnitSphere * shakeFactor, Time.deltaTime);
            if (shakeDuration <= 1.0f)
            {
                camera.orthographicSize = cameraSize;
                camera.transform.localPosition = cameraPos; 
                isShaking = false; 
            }
            shakeDuration -= Time.deltaTime; 
        }
 
        //StartSlowMo(0.1f, 1);
        //StartShake(10, 5.0f);
    }
}
