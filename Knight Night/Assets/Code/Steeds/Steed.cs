using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Steed : MonoBehaviour {

    public abstract void go();

    void Start()
    {
        go();
    }

    public abstract void unlockRTT();

    public abstract bool readyToTurn();

}
