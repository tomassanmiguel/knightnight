using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour {

    public Sprite hypeSpr;
    public Sprite lameSpr;
    public float jumpStrength = 10;
    public float excitementThreshhold = 0.3f;
    public float gravity = 0.098f;

    private float excitementLevel;
    private float startY;
    private float vspeed;

    void Start()
    {
        startY = transform.position.y;
    }
	
    void Update()
    {
        if (excitementLevel > excitementThreshhold)
        {
            GetComponent<SpriteRenderer>().sprite = hypeSpr;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = lameSpr;
        }
        transform.position += new Vector3(0, vspeed * Time.deltaTime, 0);

        if (vspeed == 0)
        {
            if (Random.value * 720 * Time.deltaTime < excitementLevel)
            {
                vspeed = jumpStrength;
            }
        }
        else
        {
            vspeed = vspeed - gravity * Time.deltaTime;
        }

        if (transform.position.y < startY)
        {
            vspeed = 0;
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }
    }

    //Sets the excitement level of this audience member from 1 to 0
    public void setExcitementLevel(float e)
    {
        if (e > 1)
        {
            e = 1;
        }
        if (e < 0)
        {
            e = 0;
        }
        excitementLevel = e;
    }
}
