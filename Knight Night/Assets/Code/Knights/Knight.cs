using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Knight : MonoBehaviour {

    public abstract void throwWeapon();

    private Combatant c;

    void Start()
    {
        c = GetComponent<Combatant>();
    }

    void Update()
    {
        if (Input.GetKeyDown(c._throwButton))
        {
            throwWeapon();
        }
    }
}
