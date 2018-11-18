﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementGenerator : MonoBehaviour {

    public GameObject prefab;
    public Canvas canvas;

    private Queue<Announcement> announcements;
    private Coroutine cr;

    void Awake()
    {
        announcements = new Queue<Announcement>();
    }

    //Announce a customizable message. Can be passed a message, how long the message stays up, message font size, message color, whether the message has an outline, and outline color.
    //Default message color is white and default outline color is black
    public void Announce(string message = "", float time = 3f, bool shake = false, float shakeDuration = 0, float shakeIntensity = 0, int fontSize = 150, Color? color = null, bool outline = true, Color? outlineColor = null)
    {
        //Default Colors
        color = color ?? Color.white;
        outlineColor = outlineColor ?? Color.black;

        announcements.Enqueue(new Announcement(message, time, fontSize, (Color)color, outline, (Color)outlineColor, shake, shakeDuration, shakeIntensity));

        if (announcements.Count == 1 && cr == null)
        {
            cr = StartCoroutine(SequenceAnnouncements());
        }
    }
    private IEnumerator SequenceAnnouncements()
    {
        Announcement current = announcements.Dequeue();
        AnnouncementText instance = Instantiate(prefab, canvas.transform).GetComponent<AnnouncementText>();
        instance.RunAnnouncement(current);
        if (current.shake)
        {
            instance.GetComponent<Shake>().startShake(current.shakeDuration, current.shakeIntensity);
        }
        yield return new WaitForSecondsRealtime(current.time + (instance.fadeTime * 2));

        if(announcements.Count > 0)
        {
            cr = StartCoroutine(SequenceAnnouncements());
        }
        else
        {
            cr = null;
        }
    }
}

public struct Announcement
{
    public string message;
    public float time;
    public int fontSize;
    public Color color, outlineColor;
    public bool outline;
    public bool shake;
    public float shakeDuration;
    public float shakeIntensity;

    public Announcement(string m, float t, int fSize, Color c, bool o, Color oc, bool s, float sd, float si)
    {
        message = m;
        time = t;
        fontSize = fSize;
        color = c;
        outline = o;
        outlineColor = oc;
        shake = s;
        shakeDuration = sd;
        shakeIntensity = si;
    }
}
