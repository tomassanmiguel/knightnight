using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Knight : MonoBehaviour {

    public Combatant combatant;
    
    public float ridingSpeed;
    public GameObject aimArrow;
    public GameObject jumpCloud;

    private bool aiming = false;
    private bool _hasJavelin = true;
    protected float _groundY;
    protected float _vSpeed;
    private bool going;
    private float aimDir = 0;
    public bool airborne = false;
    private Vector3 previouspos;
    public float aimTolerance;
    private float curaimtol;
    private bool playedAnim = false;
    private bool playingThrowAnim = false;

    public GameObject armLeft;
    public GameObject armRight;
    public GameObject bodyLeft;
    public GameObject bodyRight;
    public GameObject horse;
    public GameObject body;
    public GameObject noWeaponBody;

    public void start()
    {
        combatant = GetComponent<Combatant>();

        if (combatant == null)
        {
            Debug.Log("Knights require Combatant component");
        }

        if (combatant.player == 2)
        {
            SpriteRenderer spr = body.GetComponent<SpriteRenderer>();
            SpriteRenderer spr2 = horse.GetComponent<SpriteRenderer>();
            SpriteRenderer spr3 = noWeaponBody.GetComponent<SpriteRenderer>();
            spr.flipX = !spr.flipX;
            spr2.flipX = !spr2.flipX;
            spr3.flipX = !spr3.flipX;
            aimArrow.GetComponent<SpriteRenderer>().color = Color.blue;
            spr.sortingOrder = (int)combatant.sortingOrders.x;
            spr2.sortingOrder = (int)combatant.sortingOrders.x - 1;
            spr3.sortingOrder = (int)combatant.sortingOrders.x;
            armLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
            armRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
            bodyLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
            bodyRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
            GetComponent<BoxCollider2D>().offset = new Vector2(combatant.hitBoxXOffsets.y,0.2f);
        }
        else
        {
            SpriteRenderer spr = body.GetComponent<SpriteRenderer>();
            SpriteRenderer spr2 = horse.GetComponent<SpriteRenderer>();
            SpriteRenderer spr3 = noWeaponBody.GetComponent<SpriteRenderer>();
            aimArrow.GetComponent<SpriteRenderer>().color = Color.red;
            spr.sortingOrder = (int)combatant.sortingOrders.y;
            spr2.sortingOrder = (int)combatant.sortingOrders.y - 1;
            spr3.sortingOrder = (int)combatant.sortingOrders.y;
            armLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
            armRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
            bodyLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
            bodyRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
        }
    }

    public void update()
    {
        GameManager.instance.GetComponent<CrowdController>().increaseExcitement(combatant.player, - Time.deltaTime / 20);
        if (!going)
        {
            aimArrow.GetComponent<SpriteRenderer>().enabled = false;
            if (GameManager.instance.knightsReady)
            {
                go();
                going = true;
            }
        }
        else
        {
            if (Input.GetButtonUp(combatant.throwButton) && _hasJavelin && combatant.deadTimer == 0)
            {
                if (aiming)
                {
                    GameManager.instance.GetComponent<CrowdController>().increaseExcitement(combatant.player, 0.25f);
                    thro();
                    _hasJavelin = false;
                }
            }
            else if (Input.GetButton(combatant.throwButton) && _hasJavelin)
            {
                float newAimDir = getThrowDirection();
                if (newAimDir >= 0)
                {
                    aiming = true;
                    aimDir = newAimDir;
                    curaimtol = aimTolerance;
                }
                else
                {
                    curaimtol -= Time.deltaTime;
                    if (curaimtol < 0)
                    {
                        aiming = false;
                    }
                }
            }
            else if ((!Input.GetButton(combatant.throwButton) || aimDir < 0) && !playingThrowAnim)
            {
                curaimtol -= Time.deltaTime;
                if (curaimtol < 0)
                {
                    aiming = false;
                }
            }

            
            //I sincerely apologize for this fucking mess... I had some problems
            //with the sprites and just fixed it the dumb way
            if (aiming && combatant.deadTimer == 0)
            {
                aimArrow.GetComponent<SpriteRenderer>().enabled = true;
                aimArrow.transform.eulerAngles = new Vector3(0, 0, aimDir);

                body.GetComponent<Animator>().enabled = false;
                body.GetComponent<SpriteRenderer>().enabled = false;
                noWeaponBody.GetComponent<Animator>().enabled = false;
                noWeaponBody.GetComponent<SpriteRenderer>().enabled = false;
                if (combatant.facingLeft)
                {
                    bodyLeft.GetComponent<Animator>().enabled = true;
                    armLeft.GetComponent<Animator>().enabled = true;
                    bodyLeft.GetComponent<SpriteRenderer>().enabled = true;
                    armLeft.GetComponent<SpriteRenderer>().enabled = true;
                    bodyRight.GetComponent<Animator>().enabled = false;
                    armRight.GetComponent<Animator>().enabled = false;
                    bodyRight.GetComponent<SpriteRenderer>().enabled = false;
                    armRight.GetComponent<SpriteRenderer>().enabled = false;

                    if (!playedAnim)
                    {
                        bodyLeft.GetComponent<Animator>().Play("Body", 0, 0);
                        armLeft.GetComponent<Animator>().Play("Arm", 0, 0);
                        playedAnim = true;
                    }
                }
                else
                {
                    bodyRight.GetComponent<Animator>().enabled = true;
                    armRight.GetComponent<Animator>().enabled = true;
                    bodyRight.GetComponent<SpriteRenderer>().enabled = true;
                    armRight.GetComponent<SpriteRenderer>().enabled = true;
                    bodyLeft.GetComponent<Animator>().enabled = false;
                    armLeft.GetComponent<Animator>().enabled = false;
                    bodyLeft.GetComponent<SpriteRenderer>().enabled = false;
                    armLeft.GetComponent<SpriteRenderer>().enabled = false;

                    if (!playedAnim)
                    {
                        bodyRight.GetComponent<Animator>().Play("Body", 0, 0);
                        armRight.GetComponent<Animator>().Play("Arm", 0, 0);
                        playedAnim = true;
                    }
                }
            }
            else
            {
                playedAnim = false;
                aimArrow.GetComponent<SpriteRenderer>().enabled = false;
                if (_hasJavelin)
                {
                    body.GetComponent<Animator>().enabled = true;
                    body.GetComponent<SpriteRenderer>().enabled = true;
                    noWeaponBody.GetComponent<Animator>().enabled = false;
                    noWeaponBody.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    noWeaponBody.GetComponent<Animator>().enabled = true;
                    noWeaponBody.GetComponent<SpriteRenderer>().enabled = true;
                    body.GetComponent<Animator>().enabled = false;
                    body.GetComponent<SpriteRenderer>().enabled = false;
                }
                bodyRight.GetComponent<Animator>().enabled = false;
                armRight.GetComponent<Animator>().enabled = false;
                bodyLeft.GetComponent<Animator>().enabled = false;
                armLeft.GetComponent<Animator>().enabled = false;
                bodyRight.GetComponent<SpriteRenderer>().enabled = false;
                armRight.GetComponent<SpriteRenderer>().enabled = false;
                bodyLeft.GetComponent<SpriteRenderer>().enabled = false;
                armLeft.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (airborne)
        {
            body.GetComponent<Animator>().speed = 0;
            horse.GetComponent<Animator>().speed = 0;
        }
        else
        {
            Vector3 movement = transform.position - previouspos;
            float movementMod = Mathf.Min(2, movement.magnitude / (ridingSpeed*Time.deltaTime) + 0.3f);
            body.GetComponent<Animator>().speed = movementMod;
            horse.GetComponent<Animator>().speed = movementMod;
        }

        if (transform.position.x < combatant.invisibleWallBounds.x && combatant.deadTimer == 0)
        {
            transform.position = new Vector3(combatant.invisibleWallBounds.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > combatant.invisibleWallBounds.y && combatant.deadTimer == 0)
        {
            transform.position = new Vector3(combatant.invisibleWallBounds.y, transform.position.y, transform.position.z);
        }
        previouspos = transform.position;
    }

    private void thro()
    {
        IEnumerator coroutine;
        if (combatant.facingLeft)
        {
            bodyLeft.GetComponent<Animator>().SetBool("Throw", true);
            armLeft.GetComponent<Animator>().SetBool("Throw", true);
        }
        else
        {
            bodyRight.GetComponent<Animator>().SetBool("Throw", true);
            armRight.GetComponent<Animator>().SetBool("Throw", true);
        }
        coroutine = throwCoroutine();
        StartCoroutine(coroutine);
    }

    IEnumerator throwCoroutine()
    {
        playingThrowAnim = true;
        yield return new WaitForSeconds(0.1f);
        throwWeapon(aimDir);
        yield return new WaitForSeconds(0.25f);
        playingThrowAnim = false;
        if (combatant.facingLeft)
        {
            bodyLeft.GetComponent<Animator>().SetBool("Throw", false);
            armLeft.GetComponent<Animator>().SetBool("Throw", false);
        }
        else
        {
            bodyRight.GetComponent<Animator>().SetBool("Throw", false);
            armRight.GetComponent<Animator>().SetBool("Throw", false);
        };
        yield return null;
    }


    public abstract void throwWeapon(float aimDir);
    public abstract void turnAround();

    void go()
    {
        if (combatant.facingLeft)
        {
            StartCoroutine("TrotLeft");
        }
        else
        {
            StartCoroutine("TrotRight");
        }

        _groundY = transform.position.y;
        combatant.rtt = false;
    }

    //Knight trots right
    IEnumerator TrotRight()
    {
        float maxDist = combatant.positionBounds.y - combatant.positionBounds.x;
        float dist = combatant.positionBounds.y - transform.position.x;
        float speedMod = 0.1f;
        float desiredSpeedMod = 1;
        while ((dist > 0 || transform.position.y != _groundY || speedMod > 0.3f) && transform.position.x != combatant.invisibleWallBounds.y)
        {
            if (dist < maxDist / 5 && _vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + (dist / maxDist) * 45);
            }
            else if (dist > 4 * maxDist / 5 && _vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + ((maxDist - dist) / maxDist) * 45);
            }
            else if (_vSpeed == 0)
            {
                desiredSpeedMod = 1;
            }

            if (!aiming && dist > maxDist / 3)
            {
                float aimAxis = getThrowDirection();

                if (aimAxis == 0)
                {
                    desiredSpeedMod = desiredSpeedMod * 2;
                }
                else if (aimAxis == 45 || aimAxis == 315)
                {
                    desiredSpeedMod = desiredSpeedMod * 1.5f;
                }
                else if (aimAxis == 180)
                {
                    desiredSpeedMod = desiredSpeedMod * 0.5f;
                }
                else if (aimAxis == 135 || aimAxis == 225)
                {
                    desiredSpeedMod = desiredSpeedMod * 0.75f;
                }
            }

            if (speedMod < desiredSpeedMod && _vSpeed == 0)
            {
                speedMod += Time.deltaTime;
            }
            else if (speedMod > desiredSpeedMod && _vSpeed == 0)
            {
                speedMod -= Time.deltaTime;
            }
            transform.position = (Vector2)transform.position + Vector2.right * ridingSpeed * Time.deltaTime * speedMod;
            yield return null;
            dist = combatant.positionBounds.y - transform.position.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Knight trots left
    IEnumerator TrotLeft()
    {
        float maxDist = combatant.positionBounds.y - combatant.positionBounds.x;
        float dist = transform.position.x - combatant.positionBounds.x;
        float speedMod = 0.1f;
        float desiredSpeedMod = 1;
        while ((dist > 0 || transform.position.y != _groundY || speedMod > 0.3f) && transform.position.x != combatant.invisibleWallBounds.x)
        {
            if (dist < maxDist / 5 && _vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + (dist / maxDist) * 45);
            }
            else if (dist > 4 * maxDist / 5 && _vSpeed == 0)
            {
                desiredSpeedMod = 0.1f * (1 + ((maxDist - dist) / maxDist) * 45);
            }
            else if (_vSpeed == 0)
            {
                desiredSpeedMod = 1;
            }

            if (!aiming && dist > maxDist / 3)
            {
                float aimAxis = getThrowDirection();

                if (aimAxis == 0)
                {
                    desiredSpeedMod = desiredSpeedMod * 0.66f;
                }
                else if (aimAxis == 45 || aimAxis == 315)
                {
                    desiredSpeedMod = desiredSpeedMod * 0.8f;
                }
                else if (aimAxis == 180)
                {
                    desiredSpeedMod = desiredSpeedMod * 1.5f;
                }
                else if (aimAxis == 135 || aimAxis == 225)
                {
                    desiredSpeedMod = desiredSpeedMod * 1.25f;
                }
            }

            if (speedMod < desiredSpeedMod && _vSpeed == 0)
            {
                speedMod += Time.deltaTime * 2;
            }
            else if (speedMod > desiredSpeedMod && _vSpeed == 0)
            {
                speedMod -= Time.deltaTime * 2;
            }
            transform.position = (Vector2)transform.position + Vector2.left * ridingSpeed * Time.deltaTime * speedMod;
            yield return null;
            dist = transform.position.x - combatant.positionBounds.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Round the tilts
    IEnumerator TurnAround()
    {
        if (combatant.deadTimer == 0)
        {
            turnAround();
            SpriteRenderer spr = body.GetComponent<SpriteRenderer>();
            SpriteRenderer spr2 = horse.GetComponent<SpriteRenderer>();
            SpriteRenderer spr3 = noWeaponBody.GetComponent<SpriteRenderer>();
            spr.flipX = !spr.flipX;
            spr2.flipX = !spr2.flipX;
            spr3.flipX = !spr3.flipX;
            combatant.facingLeft = !combatant.facingLeft;

            while (combatant.facingLeft && transform.position.x > combatant.positionBounds.y)
            {
                transform.position = (Vector2)transform.position + Vector2.left * ridingSpeed * Time.deltaTime;
                yield return null;
            }
            while (!combatant.facingLeft && transform.position.x < combatant.positionBounds.x)
            {
                transform.position = (Vector2)transform.position + Vector2.right * ridingSpeed * Time.deltaTime;
                yield return null;
            }

            combatant.rtt = true;

            while (!combatant.opposingKnight.GetComponent<Combatant>().rtt)
            {
                yield return null;
            }

            combatant.opposingKnight.GetComponent<Combatant>().rtt = false;

            if (combatant.facingLeft)
            {
                spr.sortingOrder = (int)combatant.sortingOrders.x;
                spr2.sortingOrder = (int)combatant.sortingOrders.x - 1;
                spr3.sortingOrder = (int)combatant.sortingOrders.x;
                armLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
                armRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
                bodyLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
                bodyRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.x;
                GetComponent<BoxCollider2D>().offset = new Vector2(combatant.hitBoxXOffsets.y, 0.2f);
                _hasJavelin = true;
                StartCoroutine("TrotLeft");
            }
            else
            {
                spr.sortingOrder = (int)combatant.sortingOrders.y;
                spr2.sortingOrder = (int)combatant.sortingOrders.y - 1;
                spr3.sortingOrder = (int)combatant.sortingOrders.y;
                armLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
                armRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
                bodyLeft.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
                bodyRight.GetComponent<SpriteRenderer>().sortingOrder = (int)combatant.sortingOrders.y;
                _hasJavelin = true;
                GetComponent<BoxCollider2D>().offset = new Vector2(combatant.hitBoxXOffsets.x, 0.2f);
                StartCoroutine("TrotRight");
            }

            yield return null;
        }
        else
        {
            while (true)
            {
                if (combatant.facingLeft)
                {
                    transform.position = (Vector2)transform.position + Vector2.left * ridingSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position = (Vector2)transform.position + Vector2.right * ridingSpeed * Time.deltaTime;
                }
                yield return null;
            }
        }
    }

    float getThrowDirection()
    {
        float hAxis = Input.GetAxis(combatant.xAxisAim);
        float vAxis = Input.GetAxis(combatant.yAxisAim);

        if (hAxis != 0 || vAxis != 0)
        {
            if (hAxis > 0.2f)
            {
                if (vAxis > 0.2f)
                {
                    return 45;
                }
                else if (vAxis < -0.2f)
                {
                    return 315;
                }
                else
                {
                    return 0;
                }

            }
            else if (hAxis < -0.2f)
            {
                if (vAxis > 0.2f)
                {
                    return 135;
                }
                else if (vAxis < -0.2f)
                {
                    return 225;
                }
                else
                {
                    return 180;
                }

            }
            else
            {
                if (vAxis > 0.2f)
                {
                    return 90;
                }
                else if (vAxis < -0.2f)
                {
                    return 270;
                }
                else if (combatant.facingLeft)
                {
                    return 180;
                }
            }
        }
        return -1;
    }
}
