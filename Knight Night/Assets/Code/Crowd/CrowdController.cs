using UnityEngine;
using System.Collections;
using System;

public class CrowdController : MonoBehaviour
{

    //adjust this to change speed
    float speed = 4f;
    //adjust this to change how high it goes
    float height = 1.5f;

    public float id;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string parent = transform.parent.name;
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        float newY;
        if (parent == "RedCrowd")
        {
            //calculate what the new Y position will be
            newY = Mathf.Sin(Time.time * 2 * speed + id);
        }
        else
        {
            newY = Mathf.Sin(Time.time * speed + id);
        }
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY*height, pos.z);
    }
}
