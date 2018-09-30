using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : Weapon
{
    [SerializeField]
    private float _speed;


    public override GameObject spawnWeapon(Combatant c)
    {
        GameObject jav = Instantiate(c._javelin);
        return jav;
    }

    public override void weaponUpdate()
    {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
    }
}

