using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ : Steed
{

    //Private Variables
    private Combatant c;
    private float groundY;
    private float vSpeed;
    private bool rtt;
    private bool dj = true;


    public override void go()
    {
        c = GetComponent<Combatant>();
        if (c._facingLeft)
        {
            StartCoroutine("TrotLeft");
        }
        else
        {
            StartCoroutine("TrotRight");
        }

        groundY = transform.position.y;
        rtt = false;
    }

    void Update()
    {
        //Jump Logic
        if (Input.GetKeyDown(c._jumpButton))
        {
            if (transform.position.y == groundY && transform.position.x > c._positionBounds.x && transform.position.x < c._positionBounds.y)
            {
                vSpeed = c._jumpForce;
            }
            else if (dj)
            {
                vSpeed = c._jumpForce;
                dj = false;
            }
        }

        transform.Translate(0, vSpeed * Time.deltaTime, 0);

        if (vSpeed != 0)
        {
            vSpeed = vSpeed - c._gravity * Time.deltaTime;
        }

        if (transform.position.y < groundY)
        {
            vSpeed = 0;
            dj = true;
            transform.position = new Vector3(transform.position.x, groundY, 0);
        }
    }

    //Knight trots right
    IEnumerator TrotRight()
    {
        float maxDist = c._positionBounds.y - c._positionBounds.x;
        float dist = c._positionBounds.y - transform.position.x;
        float speedMod = 0.1f;
        float desiredSpeedMod = 1;
        while (dist > 0 || transform.position.y != groundY || speedMod > 0.3f)
        {
            if (dist < maxDist / 5 && vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + (dist / maxDist) * 45);
            }
            else if (dist > 4 * maxDist / 5 && vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + ((maxDist - dist) / maxDist) * 45);
            }
            else if (vSpeed == 0)
            {
                desiredSpeedMod = 1;
            }

            if (speedMod < desiredSpeedMod && vSpeed == 0)
            {
                speedMod += Time.deltaTime;
            }
            else if (speedMod > desiredSpeedMod && vSpeed == 0)
            {
                speedMod -= Time.deltaTime;
            }
            transform.position = (Vector2)transform.position + Vector2.right * c._ridingSpeed * Time.deltaTime * speedMod;
            yield return null;
            dist = c._positionBounds.y - transform.position.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Knight trots left
    IEnumerator TrotLeft()
    {
        float maxDist = c._positionBounds.y - c._positionBounds.x;
        float dist = transform.position.x - c._positionBounds.x;
        float speedMod = 0.1f;
        float desiredSpeedMod = 1;
        while (dist > 0 || transform.position.y != groundY || speedMod > 0.3f)
        {
            if (dist < maxDist / 5 && vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + (dist / maxDist) * 45);
            }
            else if (dist > 4 * maxDist / 5 && vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + ((maxDist - dist) / maxDist) * 45);
            }
            else if (vSpeed == 0)
            {
                desiredSpeedMod = 1;
            }

            if (speedMod < desiredSpeedMod && vSpeed == 0)
            {
                speedMod += Time.deltaTime;
            }
            else if (speedMod > desiredSpeedMod && vSpeed == 0)
            {
                speedMod -= Time.deltaTime;
            }
            transform.position = (Vector2)transform.position + Vector2.left * c._ridingSpeed * Time.deltaTime * speedMod;
            yield return null;
            dist = transform.position.x - c._positionBounds.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Round the tilts
    IEnumerator TurnAround()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.flipX = !spr.flipX;
        c._facingLeft = !c._facingLeft;

        while (c._facingLeft && transform.position.x > c._positionBounds.y)
        {
            transform.position = (Vector2)transform.position + Vector2.left * c._ridingSpeed / 3 * Time.deltaTime;
            yield return null;
        }
        while (!c._facingLeft && transform.position.x < c._positionBounds.x)
        {
            transform.position = (Vector2)transform.position + Vector2.right * c._ridingSpeed / 3 * Time.deltaTime;
            yield return null;
        }

        rtt = true;

        while (!c._opposingKnight.GetComponent<Steed>().readyToTurn())
        {
            yield return null;
        }

        c._opposingKnight.GetComponent<Steed>().unlockRTT();

        if (!c._facingLeft)
        {
            spr.sortingOrder = (int)c._sortingOrders.x;
            StartCoroutine("TrotRight");
        }
        else
        {
            spr.sortingOrder = (int)c._sortingOrders.y;
            StartCoroutine("TrotLeft");
        }

        yield return null;
    }

    //Both knights should turn around simultaneously
    override public bool readyToTurn()
    {
        return rtt;
    }

    override public void unlockRTT()
    {
        rtt = false;
    }
}
