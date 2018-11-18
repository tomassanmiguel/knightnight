using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelectEffect : MonoBehaviour, ISelectHandler, IDeselectHandler {

    [SerializeField] private int selectedFontSize;
    [SerializeField] [Range(0, 1)] private float deselectedOpacity;

    [SerializeField] private float transitionTime;

    private Text text;
    private int fontSize;
    private Color color;

    private Coroutine current;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        fontSize = text.fontSize;
        color = text.color;

        text.color = new Color(color.r, color.g, color.b, deselectedOpacity);
    }

    private void Start()
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject)
        {
            text.color = color;
            text.fontSize = selectedFontSize;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (current != null) StopCoroutine(current);
        current = StartCoroutine(selected());
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (current != null) StopCoroutine(current);
        current = StartCoroutine(deselected());
    }

    private IEnumerator selected()
    {
        float idx = 0f;
        float increment = 1 / (transitionTime / 0.01f);
        while (text.fontSize != selectedFontSize && text.color.a != color.a)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            text.fontSize = (int) Mathf.Lerp(fontSize, selectedFontSize, idx);
            text.color = new Color(color.r, color.g, color.b, Mathf.Lerp(deselectedOpacity, color.a, idx));
            idx += increment;
        }
    }

    private IEnumerator deselected()
    {
        float idx = 1f;
        float increment = 1 / (transitionTime / 0.01f);
        while (text.fontSize != fontSize && text.color.a != deselectedOpacity)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            text.fontSize = (int)Mathf.Lerp(fontSize, selectedFontSize, idx);
            text.color = new Color(color.r, color.g, color.b, Mathf.Lerp(deselectedOpacity, color.a, idx));
            idx -= increment;
        }
    }
}
