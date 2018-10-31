using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirLance : MonoBehaviour
{
    public Combatant combatant;

    [SerializeField]
    public GameObject javelin;
    [SerializeField]
    public float ridingSpeed;
    [SerializeField]
    public float jumpForce;
    [SerializeField]
    public float gravity;
    [SerializeField]
    public GameObject aimArrow;

    private bool aiming = false;
    private bool _hasJavelin = true;
    private float _groundY;
    private float _vSpeed;
    private bool doubleJump;

    void Start()
    {
        combatant = GetComponent<Combatant>();
        go();

        if (combatant == null)
        {
            Debug.Log("Sir Lance requires Combatant component");
        }

        doubleJump = false;
    }

    public void throwWeapon(float aimDir)
    {
        GameObject jav = Instantiate(javelin);
        jav.GetComponent<Javelin>()._otherKnight = combatant.opposingKnight;
        jav.transform.position = transform.position;
        jav.transform.Rotate(0, 0, aimDir);
    }

    void Update()
    {
        if (combatant.deadTimer == 0)
        {
            float aimDir = getThrowDirection();
            if (Input.GetButtonUp(combatant.throwButton) && _hasJavelin)
            {
                if (aimDir >= 0)
                {
                    throwWeapon(aimDir);
                    aiming = false;
                    _hasJavelin = false;
                }
            }
            else if (Input.GetButton(combatant.throwButton) && _hasJavelin)
            {
                if (aimDir >= 0)
                {
                    aiming = true;
                }
                else
                {
                    aiming = false;
                }
            }
            else
            {
                aiming = false;
            }

            if (aiming)
            {
                aimArrow.GetComponent<SpriteRenderer>().enabled = true;
                aimArrow.transform.eulerAngles = new Vector3(0, 0, aimDir);
            }
            else
            {
                aimArrow.GetComponent<SpriteRenderer>().enabled = false;
            }

            //Jump Logic
            if (Input.GetButtonDown(combatant.jumpButton) && transform.position.x > combatant.positionBounds.x && transform.position.x < combatant.positionBounds.y)
            {
                if (transform.position.y == _groundY)
                {
                    _vSpeed = jumpForce;
                }
                else if (doubleJump)
                {
                    doubleJump = false;
                    _vSpeed = jumpForce/1.5f;
                }
            }

            transform.Translate(0, _vSpeed * Time.deltaTime, 0);

            if (_vSpeed != 0)
            {
                _vSpeed = _vSpeed - gravity * Time.deltaTime;
            }

            if (transform.position.y < _groundY)
            {
                _vSpeed = 0;
                doubleJump = true;
                transform.position = new Vector3(transform.position.x, _groundY, 0);
            }
        }
    }

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
        if (combatant.deadTimer == 0)
        {
            float maxDist = combatant.positionBounds.y - combatant.positionBounds.x;
            float dist = combatant.positionBounds.y - transform.position.x;
            float speedMod = 0.1f;
            float desiredSpeedMod = 1;
            while ((dist > 0 || transform.position.y != _groundY || speedMod > 0.3f) && combatant.deadTimer == 0)
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
    }

    //Knight trots left
    IEnumerator TrotLeft()
    {
        if (combatant.deadTimer == 0)
        {
            float maxDist = combatant.positionBounds.y - combatant.positionBounds.x;
            float dist = transform.position.x - combatant.positionBounds.x;
            float speedMod = 0.1f;
            float desiredSpeedMod = 1;
            while ((dist > 0 || transform.position.y != _groundY || speedMod > 0.3f) && combatant.deadTimer == 0)
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
    }

    //Round the tilts
    IEnumerator TurnAround()
    {
        if (combatant.deadTimer == 0)
        {
            Debug.Log("TA");
            SpriteRenderer spr = GetComponent<SpriteRenderer>();
            spr.flipX = !spr.flipX;
            combatant.facingLeft = !combatant.facingLeft;

            while (combatant.facingLeft && transform.position.x > combatant.positionBounds.y)
            {
                transform.position = (Vector2)transform.position + Vector2.left * ridingSpeed / 3 * Time.deltaTime;
                yield return null;
            }
            while (!combatant.facingLeft && transform.position.x < combatant.positionBounds.x)
            {
                transform.position = (Vector2)transform.position + Vector2.right * ridingSpeed / 3 * Time.deltaTime;
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
                _hasJavelin = true;
                StartCoroutine("TrotLeft");
            }
            else
            {
                spr.sortingOrder = (int)combatant.sortingOrders.y;
                _hasJavelin = true;
                StartCoroutine("TrotRight");
            }

            yield return null;
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