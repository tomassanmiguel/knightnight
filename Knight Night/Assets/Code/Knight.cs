using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

    //Serialized Fields
    [SerializeField]
    private bool _facingLeft;
    [SerializeField]
    private Vector2 _positionBounds;
    [SerializeField]
    private float _ridingSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _throwForce;
    [SerializeField]
    private KeyCode _jumpButton;
    [SerializeField]
    private KeyCode _throwButton;
    [SerializeField]
    private string _xAxisAim;
    [SerializeField]
    private string _yAxisAim;
    [SerializeField]
    private Vector2 _sortingOrders;
    [SerializeField]
    private GameObject _opposingKnight;

    //Private Variables
    private float groundY;
    private float vSpeed;
    private bool rtt;

	// Initializations
	void Start () {
        if (_facingLeft)
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
	
	void Update ()
    {
        //Jump Logic
        if (Input.GetKeyDown(_jumpButton) && transform.position.y == groundY && transform.position.x > _positionBounds.x && transform.position.x < _positionBounds.y)
        {
            vSpeed = _jumpForce;
        }

        transform.Translate(0, vSpeed* Time.deltaTime, 0);

        if (vSpeed != 0)
        {
            vSpeed = vSpeed - _gravity * Time.deltaTime;
        }

        if (transform.position.y < groundY)
        {
            vSpeed = 0;
            transform.position = new Vector3(transform.position.x, groundY, 0);
        }
	}

    //Knight trots right
    IEnumerator TrotRight()
    {
        float maxDist = _positionBounds.y - _positionBounds.x;
        float dist = _positionBounds.y - transform.position.x;
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
            transform.position = (Vector2)transform.position + Vector2.right * _ridingSpeed * Time.deltaTime * speedMod;
            yield return null;
            dist = _positionBounds.y - transform.position.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Knight trots left
    IEnumerator TrotLeft()
    {
        float maxDist = _positionBounds.y - _positionBounds.x;
        float dist = transform.position.x - _positionBounds.x;
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
            transform.position = (Vector2)transform.position + Vector2.left * _ridingSpeed * Time.deltaTime * speedMod;
            yield return null;
            dist = transform.position.x - _positionBounds.x;
        }

        StartCoroutine("TurnAround");
        yield return null;
    }

    //Round the tilts
    IEnumerator TurnAround()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.flipX = !spr.flipX;
        _facingLeft = !_facingLeft;

        while (_facingLeft && transform.position.x > _positionBounds.y)
        {
            transform.position = (Vector2)transform.position + Vector2.left * _ridingSpeed / 3 * Time.deltaTime;
            yield return null;
        }
        while (!_facingLeft && transform.position.x < _positionBounds.x)
        {
            transform.position = (Vector2)transform.position + Vector2.right * _ridingSpeed / 3 * Time.deltaTime;
            yield return null;
        }

        rtt = true;

        while (!_opposingKnight.GetComponent<Knight>().readyToTurn())
        {
            yield return null;
        }

        _opposingKnight.GetComponent<Knight>().unlockRTT();

        if (!_facingLeft)
        {
            spr.sortingOrder = (int)_sortingOrders.x;
            StartCoroutine("TrotRight");
        }
        else
        {
            spr.sortingOrder = (int)_sortingOrders.y;
            StartCoroutine("TrotLeft");
        }

        yield return null;
    }

    //Both knights should turn around simultaneously
    public bool readyToTurn()
    {
        return rtt;
    }

    public void unlockRTT()
    {
        rtt = false;
    }
}
