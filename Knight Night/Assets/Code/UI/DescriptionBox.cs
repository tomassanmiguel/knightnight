using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionBox : MonoBehaviour {

    [SerializeField] private Text description;
    [SerializeField] private KnightCollection knightCollection;

    public void UpdateDescription(int index)
    {
        description.text = knightCollection.Collection[index].Description;
    }


}
