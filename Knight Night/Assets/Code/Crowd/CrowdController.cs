using UnityEngine;
using System.Collections;
using System;

public class CrowdController : MonoBehaviour
{
    public float id;
    public CrowdGroup parent;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //System.Random rand = new System.Random();
        string parentName = transform.parent.name;
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        float newY = 0;
        if (parentName == "RedCrowd")
        {
            float crowdThreshold = parent.redExcitement*transform.parent.childCount;

            if (id < crowdThreshold)
            {
                //calculate what the new Y position will be
                newY = Mathf.Sin(Time.time * parent.speed * parent.redExcitement + id);
            }
            //set the object's Y to the new calculated Y
            transform.position = new Vector3(pos.x, parent.redHeight + newY * parent.amp, pos.z);
        }
        else
        {
            float crowdThreshold = parent.blueExcitement*transform.parent.childCount;

            if (id < crowdThreshold)
            {
                newY = Mathf.Sin(Time.time * parent.speed * parent.blueExcitement);
            }
            //set the object's Y to the new calculated Y
            transform.position = new Vector3(pos.x, parent.blueHeight + newY * parent.amp, pos.z);
        }
        
    }
}
