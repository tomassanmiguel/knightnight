using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        targetPos = FindRightSide();
        transform.position = targetPos;
    }

    private void Update()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            prevTargetPos = targetPos;
            targetPos = FindRightSide();
            transform.position = targetPos;
            //For Smooth Movement
            /*
            if (targetPos != prevTargetPos)
            {
                if (run != null)
                {
                    StopCoroutine(run);
                }
                run = StartCoroutine(moveTo(targetPos));
            }
            */
        }
    }

    //For smooth movement
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

    private Vector3 FindLeftSide()
    {
        GameObject curr = eventSystem.currentSelectedGameObject;
        return curr.transform.position - (curr.transform.right * (curr.GetComponent<RectTransform>().sizeDelta.x / (curr.GetComponent<Image>().canvas.referencePixelsPerUnit * 2)));
        //return new Vector3(curr.transform.position.x - (curr.GetComponent<RectTransform>().sizeDelta.x / (curr.GetComponent<Image>().canvas.referencePixelsPerUnit)), curr.transform.position.y - (curr.GetComponent<RectTransform>().sizeDelta.y / (curr.GetComponent<Image>().canvas.referencePixelsPerUnit * 2)), curr.transform.position.z);
    }

    private Vector3 FindRightSide()
    {
        GameObject curr = eventSystem.currentSelectedGameObject;
        return curr.transform.position + (curr.transform.right * (curr.GetComponent<RectTransform>().sizeDelta.x / (curr.GetComponent<Image>().canvas.referencePixelsPerUnit * 2)));
        //return new Vector3(curr.transform.position.x + (curr.GetComponent<RectTransform>().sizeDelta.x / (curr.GetComponent<Image>().canvas.referencePixelsPerUnit)), curr.transform.position.y - (curr.GetComponent<RectTransform>().sizeDelta.y / (curr.GetComponent<Image>().canvas.referencePixelsPerUnit * 2)), curr.transform.position.z);
    }

}
