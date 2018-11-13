using UnityEngine;
using System.Collections;
using System;

public class CrowdController : MonoBehaviour
{
    public GameObject[] audienceMembersP1;
    public GameObject[] audienceMembersP2;
    public float p1excitement;
    public float p2excitement;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < audienceMembersP1.Length; i++)
        {
            audienceMembersP1[i].GetComponent<AudienceMember>().setExcitementLevel(p1excitement);
        }
        for (int i = 0; i < audienceMembersP2.Length; i++)
        {
            audienceMembersP2[i].GetComponent<AudienceMember>().setExcitementLevel(p2excitement);
        }
    }

    public void increaseExcitement(int player, float excitement)
    {
        if (player == 1)
        {
            p1excitement += excitement;
            if (p1excitement < 0)
            {
                p1excitement = 0;
            }
        }
        else
        {
            p2excitement += excitement;
            if (p2excitement < 0)
            {
                p2excitement = 0;
            }
        }
    }
}
