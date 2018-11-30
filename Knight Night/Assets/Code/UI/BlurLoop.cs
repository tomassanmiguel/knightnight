using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurLoop : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private bool flip;

    RectTransform rectTransform;
    Vector2 offset = Vector2.zero;
    Vector2 origPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        origPos = rectTransform.anchoredPosition;
    }

    private void FixedUpdate()
    {
        offset.x += speed;
        if(offset.x > 1920f)
        {
            offset.x = 0;
        }
        if (flip)
        {
            rectTransform.anchoredPosition = origPos - offset;
        }
        else
        {
            rectTransform.anchoredPosition = origPos + offset;
        }
    }
}
