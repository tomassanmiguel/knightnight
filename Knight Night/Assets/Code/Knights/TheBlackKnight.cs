using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBlackKnight : Knight
{
    public GameObject javelin;
    public float jumpForce;
    public float gravity;
    public float diveSpeed;
    private bool diving;

    void Start()
    {
        start();
        Color c = new Color(0.1f, 0.1f, 0.1f);
        body.GetComponent<SpriteRenderer>().color = c;
        armLeft.GetComponent<SpriteRenderer>().color = c;
        armRight.GetComponent<SpriteRenderer>().color = c;
        bodyLeft.GetComponent<SpriteRenderer>().color = c;
        bodyRight.GetComponent<SpriteRenderer>().color = c;
        noWeaponBody.GetComponent<SpriteRenderer>().color = c;
        diving = false;
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
                SoundEffectsManager.instance.playSound(27, false);
                GameManager.instance.GetComponent<CrowdController>().increaseExcitement(combatant.player, 0.05f);
                GameObject jc = Instantiate(jumpCloud);
                if (combatant.facingLeft)
                    jc.transform.position = transform.position + new Vector3(0, -1.75f, 0);
                else
                    jc.transform.position = transform.position + new Vector3(0, -1.75f, -1f);
            }
            else
            {
                diving = true;
            }
        }

        if (Input.GetButtonUp(combatant.jumpButton))
        {
            diving = false;
        }

        if (diving && transform.position.y > _groundY)
        {
            _vSpeed += diveSpeed * Time.deltaTime;
        }
        transform.Translate(0, _vSpeed * Time.deltaTime, 0);

        if (_vSpeed != 0)
        {
            _vSpeed = _vSpeed - gravity * Time.deltaTime;
            airborne = true;
        }

        if (transform.position.y < _groundY)
        {
            if (_vSpeed != 0)
            {
                airborne = false;
            }
            _vSpeed = 0;
            transform.position = new Vector3(transform.position.x, _groundY, 0);
        }
    }
    public override void throwWeapon(float aimDir)
    {
        GameObject jav = Instantiate(javelin);
        SoundEffectsManager.instance.playSound(9, false);
        jav.GetComponent<Javelin>()._otherKnight = combatant.opposingKnight;
        jav.transform.position = transform.position + new Vector3(0, 1.3f, 0);
        jav.transform.Rotate(0, 0, aimDir);
    }

    //Dummy
    public override void turnAround()
    {
        return;
    }
}