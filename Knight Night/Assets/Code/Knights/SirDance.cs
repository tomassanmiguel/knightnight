using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirDance : Knight
{
    public GameObject discus;
    public float jumpForce;
    public float gravity;
    private bool _canDash = true;
    public float dashSpeed;
    public float dashDistance;

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
        if (Input.GetButtonDown(combatant.jumpButton) && _canDash && transform.position.y > _groundY)
        {
            _vSpeed = 0.1f;
            StartCoroutine("dash");
            _canDash = false;
        }
        if (Input.GetButtonDown(combatant.jumpButton) && transform.position.x > combatant.positionBounds.x && transform.position.x < combatant.positionBounds.y)
        {
            if (transform.position.y == _groundY)
            {
                _vSpeed = jumpForce;
            }
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
        GameObject disc = Instantiate(discus);
        SoundEffectsManager.instance.playSound(9, false);
        disc.GetComponent<Discus>()._otherKnight = combatant.opposingKnight;
        disc.transform.position = transform.position;
        disc.transform.Rotate(0, 0, aimDir);
    }

    public override void turnAround()
    {
        _canDash = true;
    }

    IEnumerator dash()
    {                                 //Dash function
        if (combatant.facingLeft)
        {
            float endDashPos = transform.position.x - dashDistance;
            while (transform.position.x > endDashPos)
            {
                transform.position = (Vector2)transform.position + Vector2.left * dashSpeed * Time.deltaTime;
                yield return null;
            }
        }
        else
        {      //Sir Dance is facing right
            float endDashPos = transform.position.x + dashDistance;
            while (transform.position.x < endDashPos)
            {
                transform.position = (Vector2)transform.position + Vector2.right * dashSpeed * Time.deltaTime;
                yield return null;
            }
        }

        yield return null;
    }
}