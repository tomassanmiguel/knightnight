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

    // Use this for initialization
    void Start()
    {
        cameraPos = camera.transform.position;
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
            camera.transform.position = cameraPos;
            camera.orthographicSize *= 2; 
            isSlowMo = false;
            Time.timeScale = 1.0f;
    }

    public void StartSlowMo(System.Single factor, int playerNum)
    {
        // placeholder event to trigger slowMo 
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isSlowMo)
                EndSlowMo(); 
            else
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
           

            /* 
            // get knight position offset 
            Vector3 playerPos;
            if (playerNum == 1)
                playerPos = knight1.transform.position;
            else
                playerPos = knight2.transform.position;

            if (isSlowMo)
            {
                Time.timeScale = 1.0f;
                // reset camera 
                camera.orthographicSize *= 2; //Mathf.Lerp(camera.orthographicSize, camera.orthographicSize  zoomFactor, Time.deltaTime);
                camera.transform.position = cameraPos;
                isSlowMo = false;
            }
            else
            {
                Time.timeScale = slowFactor;
                // zoom in on indicated knight 
                camera.orthographicSize /= 2;

                // camera positioning - make sure to not go offscreen
                if (playerPos.x > 14f)
                    newCameraPos = new Vector3(14f, -3.0f, cameraPos.z);
                else if (playerPos.x < -14f)
                    newCameraPos = new Vector3(-14f, -3.0f, cameraPos.z);
                else 
                    newCameraPos = new Vector3(playerPos.x, -3.0f, cameraPos.z);
                while (camera.transform.position != newCameraPos)
                {
                    camera.transform.position = Vector3.Lerp(camera.transform.position, newCameraPos, 2.0f);
                }
                // camera.transform.position = Vector3.Lerp(cameraPos, newCameraPos, Time.deltaTime * 3.0f); 
                isSlowMo = true;
            }
            Time.fixedDeltaTime = 0.02f * Time.timeScale; */
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomDuration > 0 && isSlowMo)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, newCameraPos, Time.deltaTime / slowFactor * 8.0f);
        }
        if (camera.orthographicSize > cameraZoom && isSlowMo)
        {
            camera.orthographicSize -= 1.0f;
        }
        if (shakeDuration > 0 && isShaking)
        {
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition,
              cameraPos + UnityEngine.Random.insideUnitSphere * shakeFactor, Time.deltaTime);
            if (shakeDuration <= 1.0f)
            {
                camera.transform.position = cameraPos;
                isShaking = false; 
            }
            shakeDuration -= Time.deltaTime; 
        }
        StartSlowMo(0.1f, 1);
        StartShake(10, 3.0f);
    }
}
