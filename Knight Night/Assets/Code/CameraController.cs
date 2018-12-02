using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float desiredOrthoSize;
    public Vector3 desiredPosition;
    public float movementSpeed;
    public float zoomSpeed;
    public static CameraController instance;
    public Vector3 curPosition;
    private float minY;
    private float maxX;
    private float minX;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        desiredOrthoSize = Camera.main.orthographicSize;
        desiredPosition = transform.position;
        curPosition = transform.position;
        minY = transform.position.y - Camera.main.orthographicSize / 16 * 9;
        minX = transform.position.x - Camera.main.orthographicSize;
        maxX = transform.position.x + Camera.main.orthographicSize;
    }

    void Update()
    {
        /*
        Vector3 distance = transform.position - desiredPosition;
        if (distance.magnitude > movementSpeed*Time.deltaTime*4)
        {
            transform.Translate(-1 * distance/distance.magnitude * Time.deltaTime * zoomSpeed);

            if (transform.position.y - Camera.main.orthographicSize / 16 * 9 < minY)
            {
                transform.position = new Vector3(transform.position.x, minY + Camera.main.orthographicSize / 16 * 9, transform.position.z);
            }

            if (transform.position.x + Camera.main.orthographicSize > maxX)
            {
                transform.position = new Vector3(maxX - Camera.main.orthographicSize, transform.position.y, transform.position.z);
            }
            if (transform.position.x - Camera.main.orthographicSize < minX)
            {
                transform.position = new Vector3(maxX + Camera.main.orthographicSize, transform.position.y, transform.position.z);
            }

            curPosition = transform.position;
        }

        if (Mathf.Abs(desiredOrthoSize - Camera.main.orthographicSize) > zoomSpeed * Time.deltaTime * 2)
        {
            if (desiredOrthoSize > Camera.main.orthographicSize)
            {
                Camera.main.orthographicSize += zoomSpeed * Time.deltaTime;
            }
            else
            {
                Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime;
            }
        }
        */
    }

    public void newZoom(Vector3 pos, float orthoSize)
    {
        desiredOrthoSize = orthoSize;
        desiredPosition = pos;
    }
    
}
