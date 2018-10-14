using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInputData", menuName = "Player Input Data")]
public class PlayerInputData : ScriptableObject {

    [SerializeField] private string horizontal;
    [SerializeField] private string vertical;
    [SerializeField] private string fire1;
    [SerializeField] private string fire2;
    [SerializeField] private string fire3;

    public string Horizontal
    {
        get
        {
            return horizontal;
        }
    }

    public string Vertical
    {
        get
        {
            return vertical;
        }
    }

    public string Fire1
    {
        get
        {
            return fire1;
        }
    }

    public string Fire2
    {
        get
        {
            return fire2;
        }
    }

    public string Fire3
    {
        get
        {
            return fire3;
        }
    }
}
