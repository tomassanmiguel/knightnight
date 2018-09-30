using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool isSlowMo = false;
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

    public void StartShake(int shakeFactor, System.Single shakeDuration)
    {
        // placeholder event to trigger shake 
        while (shakeDuration > 0)
        {
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition,
                       cameraPos + UnityEngine.Random.insideUnitSphere * shakeFactor, Time.deltaTime * 3);
            shakeDuration -= Time.deltaTime * shakeFactor;
        }
        camera.transform.position = cameraPos;
    }

    public void StartSlowMo(System.Single slowFactor, int playerNum, float zoomFactor)
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
                // camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camera.orthographicSize  zoomFactor, Time.deltaTime);
                camera.transform.position = cameraPos;
                isSlowMo = false;
            }
            else
            {
                Time.timeScale = slowFactor;
                // zoom in on indicated knight 
                // camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camera.orthographicSize * zoomFactor, Time.deltaTime * 3.0f);
                // Vector3 offset = cameraPos - playerPos;
                // camera zoom-in
                camera.transform.position = Vector3.Lerp(cameraPos, playerPos, Time.deltaTime * 3.0f);
                // camera.transform.position = new Vector3(-zoomFactor*playerPos.x/2, -zoomFactor*playerPos.y/2, cameraPos.z);
                isSlowMo = true;
            }
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
    // Update is called once per frame
    void Update()
    {
        StartSlowMo(0.1f, 1, 2.0f);
        StartShake(3, 5f);
    }
}
