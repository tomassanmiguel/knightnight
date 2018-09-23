using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

    public string[] names;

    private int currIndex;


    public int getSelection()
    {
        return currIndex;
    }
}
