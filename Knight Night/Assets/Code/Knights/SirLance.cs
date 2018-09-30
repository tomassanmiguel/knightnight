using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirLance : Knight
{

    Combatant combatant;

    void Start()
    {
        combatant = GetComponent<Combatant>();
    }

    public override void throwWeapon()
    {
        GameObject weapon = combatant.weapon.spawnWeapon(combatant);
        weapon.transform.position = transform.position;

        float hAxis = Input.GetAxis(combatant._xAxisAim);
        float vAxis = Input.GetAxis(combatant._yAxisAim);
        if (hAxis > 0.2f)
        {
            if (vAxis > 0.2f)
            {
                weapon.transform.Rotate(0, 0, 45);
            }
            else if (vAxis < -0.2f)
            {
                weapon.transform.Rotate(0, 0, 315);
            }

        }
        else if (hAxis < -0.2f)
        {
            if (vAxis > 0.2f)
            {
                weapon.transform.Rotate(0, 0, 135);
            }
            else if (vAxis < -0.2f)
            {
                weapon.transform.Rotate(0, 0, 225);
            }
            else
            {
                weapon.transform.Rotate(0, 0, 180);
            }

        }
        else
        {
            if (vAxis > 0.2f)
            {
                weapon.transform.Rotate(0, 0, 90);
            }
            else if (vAxis < -0.2f)
            {
                weapon.transform.Rotate(0, 0, 270);
            }
            else if (combatant._facingLeft)
            {
                weapon.transform.Rotate(0, 0, 180);
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(combatant._throwButton) && combatant.hasJavelin)
        {
            throwWeapon();
            combatant.hasJavelin = false;
        }
    }

}
