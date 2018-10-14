using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirLance : MonoBehaviour
{
    public Combatant combatant;

    [SerializeField]
    public GameObject javelin;
    [SerializeField]
    public bool facingLeft;
    [SerializeField]
    public Vector2 positionBounds;
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

    void Start()
    {
        combatant = GetComponent<Combatant>();
        go();

        if (combatant == null)
        {
            Debug.Log("Sir Lance requires Combatant component");
        }
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
        float aimDir = getThrowDirection();
        if (Input.GetKeyUp(combatant.throwButton) && _hasJavelin)
        {
            if (aimDir >= 0)
            {
                throwWeapon(aimDir);
                aiming = false;
                _hasJavelin = false;
            }
        }
        else if (Input.GetKey(combatant.throwButton) && _hasJavelin)
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
        if (Input.GetKeyDown(combatant.jumpButton) && transform.position.y == _groundY && transform.position.x > positionBounds.x && transform.position.x < positionBounds.y)
        {
            _vSpeed = jumpForce;
        }

        transform.Translate(0, _vSpeed * Time.deltaTime, 0);

        if (_vSpeed != 0)
        {
            _vSpeed = _vSpeed - gravity * Time.deltaTime;
        }

        if (transform.position.y < _groundY)
        {
            _vSpeed = 0;
            transform.position = new Vector3(transform.position.x, _groundY, 0);
        }
    }

    void go()
    {
        if (facingLeft)
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
        Debug.Log("TR");
        float maxDist = positionBounds.y - positionBounds.x;
        float dist = positionBounds.y - transform.position.x;
        float speedMod = 0.1f;
        float desiredSpeedMod = 1;
        while (dist > 0 || transform.position.y != _groundY || speedMod > 0.3f)
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
            dist = positionBounds.y - transform.position.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Knight trots left
    IEnumerator TrotLeft()
    {
        Debug.Log("TL");
        float maxDist = positionBounds.y - positionBounds.x;
        float dist = transform.position.x - positionBounds.x;
        float speedMod = 0.1f;
        float desiredSpeedMod = 1;
        while (dist > 0 || transform.position.y != _groundY || speedMod > 0.3f)
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

            if (speedMod < desiredSpeedMod && _vSpeed == 0)
            {
                speedMod += Time.deltaTime;
            }
            else if (speedMod > desiredSpeedMod && _vSpeed == 0)
            {
                speedMod -= Time.deltaTime;
            }
            transform.position = (Vector2)transform.position + Vector2.left * ridingSpeed * Time.deltaTime * speedMod;
            yield return null;
            dist = transform.position.x - positionBounds.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Round the tilts
    IEnumerator TurnAround()
    {
        Debug.Log("TA");
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.flipX = !spr.flipX;
        facingLeft = !facingLeft;

        while (facingLeft && transform.position.x > positionBounds.y)
        {
            transform.position = (Vector2)transform.position + Vector2.left * ridingSpeed / 3 * Time.deltaTime;
            yield return null;
        }
        while (!facingLeft && transform.position.x < positionBounds.x)
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

        if (facingLeft)
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

    float getThrowDirection()
    {
        bool u = Input.GetKey(combatant.aimUpButton);
        bool d = Input.GetKey(combatant.aimDownButton);
        bool r = Input.GetKey(combatant.aimRightButton);
        bool l = Input.GetKey(combatant.aimLeftButton);
        float hAxis = Input.GetAxis(combatant.xAxisAim);
        float vAxis = Input.GetAxis(combatant.yAxisAim);

        if (u || d || r || l)
        {
            if (r)
            {
                if (u)
                {
                    return 45;
                }
                else if (d)
                {
                    return 315;
                }
                else
                {
                    return 0;
                }
            }
            else if (l)
            {
                if (u)
                {
                    return 135;
                }
                else if (d)
                {
                    return 225;
                }
                else
                {
                    return 180;
                }
            }
            else if (u)
            {
                return 90;
            }
            else if (d)
            {
                return 270;
            }
        }

        else if (hAxis != 0 || vAxis != 0)
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
                else if (facingLeft)
                {
                    return 180;
                }
            }
        }
        return -1;
    }
}