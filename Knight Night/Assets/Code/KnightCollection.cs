using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KnightCollection", menuName = "Knight Collection")]
public class KnightCollection : ScriptableObject {

    [SerializeField] private KnightData[] collection;

    public KnightData[] Collection
    {
        get
        {
            return collection;
        }
    }
}
