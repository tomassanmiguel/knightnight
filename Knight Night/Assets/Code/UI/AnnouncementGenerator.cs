using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementGenerator : MonoBehaviour {

    public GameObject prefab;

    private Queue<Announcement> announcements;

    //Announce a customizable message. Can be passed a message, how long the message stays up, message font size, message color, whether the message has an outline, and outline color.
    //Default message color is white and default outline color is black
    public void Announce(string message = "", float time = 3f, int fontSize = 150, Color? color = null, bool outline = true, Color? outlineColor = null)
    {
        //Default Colors
        color = color ?? Color.white;
        outlineColor = outlineColor ?? Color.black;

        announcements.Enqueue(new Announcement(message, time, fontSize, (Color)color, outline, (Color)outlineColor));

        if(announcements.Count == 1)
        {
            StartCoroutine(SequenceAnnouncements());
        }
    }

    private IEnumerator SequenceAnnouncements()
    {
        Announcement current = announcements.Dequeue();
        AnnouncementText instance = Instantiate(prefab).GetComponent<AnnouncementText>();
        instance.RunAnnouncement(current);
        yield return new WaitForSecondsRealtime(current.time + (instance.fadeTime * 2));

        if(announcements.Count > 0)
        {
            StartCoroutine(SequenceAnnouncements());
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

    public Announcement(string m, float t, int fSize, Color c, bool o, Color oc)
    {
        message = m;
        time = t;
        fontSize = fSize;
        color = c;
        outline = o;
        outlineColor = oc;
    }
}
