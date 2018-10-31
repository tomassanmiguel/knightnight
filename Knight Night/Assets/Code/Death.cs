using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    //[SerializeField]
    //private Vector2 _positionBounds;
    //[SerializeField]
    //private float _gravity;

    [SerializeField]
    private GameObject _opposingKnight;
    [SerializeField]
    private GameObject _myKnight;
    [SerializeField]
    private Vector2 _positionBounds;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _gravity;


    private bool coll = false;
    private float xMove;
    private float vSpeed;
    private float groundY = -6;

    //NOTE: This needs to be added to Knight.cs file for it to work//
    
    /*
    //initialization// 
    private bool trotting = true;
    
    public void StopMe()
    {
        if (trotting)
        {
            StopCoroutine("TrotLeft");
            StopCoroutine("TrotRight");
            trotting = false;
            _opposingKnight.GetComponent<Knight>().StopMe();
        }
    }
    */


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            Vector2 pos = col.transform.position;
            Vector2 currpos = transform.position;
            xMove = pos.x - currpos.x;
            vSpeed = pos.y - currpos.y;
            coll = true;
            Debug.Log("I sense collision");
            col.GetComponent<Knight>().StopMe();
            StartCoroutine("Death1");
        }

    }

    public IEnumerator Death1()
    { 
        float timer = 0;
        float speedMod = 3;
        vSpeed = 5 * vSpeed;
        float dist = _opposingKnight.transform.position.x - _positionBounds.x;
        while ( dist > 0 && vSpeed !=0 && (_opposingKnight.transform.position.y > groundY || coll))
        {
            coll = false;
            vSpeed = vSpeed - (_gravity * Time.deltaTime);
            speedMod -= Time.deltaTime;
            
            _opposingKnight.transform.position = (Vector2)_opposingKnight.transform.position + Vector2.up * vSpeed * Time.deltaTime + Vector2.right * xMove * Time.deltaTime;
            yield return null;
            dist = _positionBounds.y - _opposingKnight.transform.position.x;
            timer += Time.deltaTime;
        }

        if (_opposingKnight.transform.position.y <= groundY)
        {
            vSpeed = 0;
            _opposingKnight.transform.position = new Vector3(_opposingKnight.transform.position.x, groundY, 0);
        }

        while (timer < 3)
        {
            speedMod -= Time.deltaTime;
            Debug.Log(speedMod);
            _opposingKnight.transform.position = (Vector2)_opposingKnight.transform.position + Vector2.right * xMove * speedMod * Time.deltaTime;
            yield return null;
            timer += Time.deltaTime;
        }

        yield return null;
    }

}
