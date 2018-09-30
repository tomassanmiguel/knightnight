using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public int eventSystemIndex;
    public float moveSpeed;

    private MultiEventSystem eventSystem;

    private Vector3 targetPos;
    private Vector3 prevTargetPos;
    private Coroutine run;

    private void Awake()
    {
        eventSystem = MultiEventSystem.GetMultiEventSystem(eventSystemIndex);
        targetPos = transform.position;
    }

    private void Update()
    {
        targetPos = eventSystem.currentSelectedGameObject.transform.position;
        if(targetPos != prevTargetPos)
        {
            if(run != null)
            {
                StopCoroutine(run);
                StartCoroutine(moveTo(targetPos));
            }

        }
        prevTargetPos = targetPos;
    }

    private IEnumerator moveTo(Vector3 position)
    {
        while(transform.position != position)
        {
            Vector3.MoveTowards(transform.position, position, moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
