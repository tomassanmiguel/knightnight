﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirExpanse : Knight
{
    public GameObject lightsaber;
    public float jumpForce;
    public float gravity;
    public float floatingSpeed;

    void Start()
    {
        start();
        if (combatant == null)
        {
            Debug.Log("Sir Lance requires Combatant component");
        }
        
        _groundY = transform.position.y;
    }

    void Update()
    {
        update();
        //Jump Logic
        if (Input.GetButtonDown(combatant.jumpButton) && transform.position.x > combatant.positionBounds.x && transform.position.x < combatant.positionBounds.y)
        {
            if (transform.position.y == _groundY)
            {
                _vSpeed = jumpForce;
                GameManager.instance.GetComponent<CrowdController>().increaseExcitement(combatant.player, 0.05f);
                SoundEffectsManager.instance.playSound(27, false);
            }
        }

        if (Input.GetButton(combatant.jumpButton))
        {
            _vSpeed = Mathf.Max(floatingSpeed * -1, _vSpeed);
        }

        transform.Translate(0, _vSpeed * Time.deltaTime, 0);

        if (_vSpeed != 0)
        {
            _vSpeed = _vSpeed - gravity * Time.deltaTime;
            airborne = true;
        }

        if (transform.position.y < _groundY)
        {
            _vSpeed = 0;
            airborne = false;
            transform.position = new Vector3(transform.position.x, _groundY, 0);
        }
    }
    public override void throwWeapon(float aimDir)
    {
        GameObject saber = Instantiate(lightsaber);
        saber.GetComponent<LightSaber>()._otherKnight = combatant.opposingKnight;
        saber.transform.position = transform.position + new Vector3(0, 0.2f, 0);
        saber.transform.Rotate(0, 0, aimDir);
    }

    //Dummy
    public override void turnAround()
    {
        return;
    }
}