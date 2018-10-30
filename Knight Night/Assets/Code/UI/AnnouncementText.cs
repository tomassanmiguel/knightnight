using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementText : MonoBehaviour {

    public float fadeTime = 0.5f;

    private Text text;
    private Outline textOutline;

    private void Awake()
    {
        text = GetComponent<Text>();
        textOutline = GetComponent<Outline>();
        text.canvasRenderer.SetAlpha(0);
    }

    //Runs a customizable announcement. Can be passed a message, how long the message stays up, message font size, message color, whether the message has an outline, and outline color.
    //Default message color is white and default outline color is black
    public void RunAnnouncement(string message = "", float time = 3f, int fontSize = 150, Color? color = null, bool outline = true, Color? outlineColor = null)
    {
        //Default Colors
        color = color ?? Color.white;
        outlineColor = outlineColor ?? Color.black;

        //Setup text component
        text.text = message;
        text.fontSize = fontSize;
        text.color = (Color)color;
        if (outline)
        {
            textOutline.effectColor = (Color)outlineColor;
        }
        else
        {
            textOutline.enabled = false;
        }
        StartCoroutine(StartAnnouncement(time));
    }

    //Runs an announcement
    public void RunAnnouncement(Announcement announcement)
    {
        text.text = announcement.message;
        text.fontSize = announcement.fontSize;
        text.color = announcement.color;
        if (announcement.outline)
        {
            textOutline.effectColor = announcement.outlineColor;
        }
        else
        {
            textOutline.enabled = false;
        }
        StartCoroutine(StartAnnouncement(announcement.time));
    }

    private IEnumerator StartAnnouncement(float time)
    {
        //Fade in
        float idx = 0;
        float increment = 1 / (fadeTime / 0.01f);
        while(text.canvasRenderer.GetAlpha() < 1)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            idx += increment;
            text.canvasRenderer.SetAlpha(idx);
        }

        yield return new WaitForSecondsRealtime(time);

        //Fade out
        while (text.canvasRenderer.GetAlpha() > 0)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            idx -= increment;
            text.canvasRenderer.SetAlpha(idx);
        }

        Destroy(gameObject);
    }
}
