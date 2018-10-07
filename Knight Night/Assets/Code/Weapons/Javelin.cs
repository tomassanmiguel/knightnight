using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Javelin : Weapon
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timeToDestroy = 5;

    public GameObject _otherKnight;
    private float _createTime;


    public override GameObject spawnWeapon(Combatant c, GameObject other)
    {
        GameObject jav = Instantiate(c._javelin);
        jav.GetComponent<Javelin>()._otherKnight = other;
        _otherKnight = other;
        return jav;
    }

    public override void weaponUpdate()
    {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
        if (Time.time - _createTime > _timeToDestroy)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        _createTime = Time.time;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _otherKnight)
        {
            Debug.Log("HI");
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
}

