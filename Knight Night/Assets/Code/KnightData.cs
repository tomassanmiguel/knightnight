using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KnightData", menuName = "Knight Data")]
public class KnightData : ScriptableObject {

    [SerializeField] private string knightName;
    [SerializeField] private string description;
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
}
