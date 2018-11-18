using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New KnightData", menuName = "Knight Data")]
public class KnightData : ScriptableObject {

    [SerializeField] private string knightName;
    [SerializeField] private string description;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite charSelectImage;
    [SerializeField] private Sprite portrait;
    //Maybe reference to prefab

    public string KnightName
    {
        get
        {
            return knightName;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }

    public Sprite CharSelectImage
    {
        get
        {
            return charSelectImage;
        }
    }

    public Sprite Portrait
    {
        get
        {
            return portrait;
        }
    }
}
