using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpr : MonoBehaviour {

    public SpriteRenderer[] s;

    public void flip()
    {
        for (int i = 0; i < s.Length; i++)
        {
            s[i].flipX = !s[i].flipX;
        }
    }
}
