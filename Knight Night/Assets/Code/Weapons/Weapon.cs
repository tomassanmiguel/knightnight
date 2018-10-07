using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public abstract void weaponUpdate();
    public abstract GameObject spawnWeapon(Combatant c, GameObject other);

    void Update()
    {
        weaponUpdate();
    }
}
