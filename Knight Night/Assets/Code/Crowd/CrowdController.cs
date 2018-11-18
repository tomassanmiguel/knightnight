using UnityEngine;
using System.Collections;
using System;

public class CrowdController : MonoBehaviour
{
    public float id;
    public CrowdGroup parent;

    // Use this for initialization
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        string parentName = transform.parent.name;

        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        float newY = 0;

        if (parentName == "knight1Crowd")
        {
            float crowdThreshold = parent.knight1Excitement * transform.parent.childCount;

            if (id < crowdThreshold)
            {
                //calculate what the new Y position will be
                newY = Mathf.Sin(Time.time * parent.speed * parent.knight1Excitement + id);
            }
            //set the object's Y to the new calculated Y
            transform.position = new Vector3(pos.x, parent.knight1Height + newY * parent.amp, pos.z);
        }
        // knight2 crowd 
        else
        {
            float crowdThreshold = parent.knight2Excitement * transform.parent.childCount;

            if (id < crowdThreshold)
            {
                newY = Mathf.Sin(Time.time * parent.speed * parent.knight2Excitement);
            }
            //set the object's Y to the new calculated Y
            transform.position = new Vector3(pos.x, parent.knight2Height + newY * parent.amp, pos.z);
        }
        
    }
}
