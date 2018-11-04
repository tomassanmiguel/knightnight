﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Highlighter : MonoBehaviour {

    public int eventSystemIndex;
    public bool useDefaultEventSystem;
    public float moveSpeed;

    private EventSystem eventSystem;

    private Vector3 targetPos;
    private Vector3 prevTargetPos;
    private Coroutine run;

    private void Awake()
    {
        if (useDefaultEventSystem)
        {
            eventSystem = GameObject.Find("DefaultEventSystem").GetComponent<EventSystem>();
        }
        else
        {
            eventSystem = MultiEventSystem.GetMultiEventSystem(eventSystemIndex);
        }
        targetPos = transform.position;
    }

    private void Start()
    {
        targetPos = eventSystem.currentSelectedGameObject.transform.position;
        transform.position = targetPos;
    }

    private void Update()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            prevTargetPos = targetPos;
            targetPos = eventSystem.currentSelectedGameObject.transform.position;
            if (targetPos != prevTargetPos)
            {
                if (run != null)
                {
                    StopCoroutine(run);
                }
                run = StartCoroutine(moveTo(targetPos));
            }
        }
    }

    private IEnumerator moveTo(Vector3 position)
    {
        //Swap this to sin/cos curved movement later
        float idx = 0;
        while(transform.position != position)
        {
            transform.position = Vector3.Lerp(transform.position, position, idx);
            yield return new WaitForSeconds(0.01f);
            idx += moveSpeed;
        }
    }

}