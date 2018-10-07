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
        if (Input.GetKeyDown(KeyCode.T))
        {
            isShaking = true; 
            shakeFactor = factor;
            shakeDuration = duration;
        }
        /*
        // placeholder event to trigger shake 
        while (shakeDuration > 0)
        {
            //float x = UnityEngine.Random.Range(-1f, 1f) * shakeFactor;
            //float y = UnityEngine.Random.Range(-1f, 1f) * shakeFactor;
            //camera.transform.localPosition = new Vector3(x, y, cameraPos.z); 
       
            shakeDuration -= Time.deltaTime;
            yield return null; 
        }
        camera.transform.position = cameraPos;
        */ 
    }

    public void EndSlowMo()
    {
        camera.transform.position = cameraPos;
        isSlowMo = false;
        Time.timeScale = 1.0f;
    }

    public void StartSlowMo(System.Single slowFactor, int playerNum)
    {
        // placeholder event to trigger slowMo 
        if (Input.GetKeyDown(KeyCode.S))
        {
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
                Vector3 newCameraPos;
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
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
