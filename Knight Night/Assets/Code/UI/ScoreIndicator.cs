using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreIndicator : MonoBehaviour {

    [SerializeField] private Image[] indicators;
    [SerializeField] private Sprite empty;
    [SerializeField] private Sprite full;

    private void Awake()
    {
        ResetScore();
    }

    //Displays the score through indicators
    public void ShowScore(int score)
    {
        if(score > indicators.Length)
        {
            return;
            //Debug.LogError("ERROR: Score is larger than number of indicators");
        }
        else
        {
            for(int i = 0; i < score; i++)
            {
                indicators[i].sprite = full;
            }
            for(int i = score; i < indicators.Length; i++)
            {
                indicators[i].sprite = empty;
            }
        }
    }

    //Resets the score indicators
    public void ResetScore()
    {
        foreach (Image item in indicators)
        {
            item.sprite = empty;
        }
    }
}
