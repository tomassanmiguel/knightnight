using UnityEngine;
using System.Collections;

public class CrowdController : MonoBehaviour
{

    //adjust this to change speed
    public float speed;
    //adjust this to change how high it goes
    public float height;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z) * height;
    }
}
