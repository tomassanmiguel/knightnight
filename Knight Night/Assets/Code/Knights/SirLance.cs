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

    public void throwWeapon()
    {
        GameObject jav = Instantiate(javelin);
        jav.GetComponent<Javelin>()._otherKnight = combatant.opposingKnight;
        jav.transform.position = transform.position;

        float hAxis = Input.GetAxis(combatant.xAxisAim);
        float vAxis = Input.GetAxis(combatant.yAxisAim);
        if (hAxis > 0.2f)
        {
            if (vAxis > 0.2f)
            {
                jav.transform.Rotate(0, 0, 45);
            }
            else if (vAxis < -0.2f)
            {
                jav.transform.Rotate(0, 0, 315);
            }

        }
        else if (hAxis < -0.2f)
        {
            if (vAxis > 0.2f)
            {
                jav.transform.Rotate(0, 0, 135);
            }
            else if (vAxis < -0.2f)
            {
                jav.transform.Rotate(0, 0, 225);
            }
            else
            {
                jav.transform.Rotate(0, 0, 180);
            }

        }
        else
        {
            if (vAxis > 0.2f)
            {
                jav.transform.Rotate(0, 0, 90);
            }
            else if (vAxis < -0.2f)
            {
                jav.transform.Rotate(0, 0, 270);
            }
            else if (facingLeft)
            {
                jav.transform.Rotate(0, 0, 180);
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(combatant.throwButton) && _hasJavelin)
        {
            throwWeapon();
            _hasJavelin = false;
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
}