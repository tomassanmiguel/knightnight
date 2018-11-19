using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Discus : Weapon
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _turnAroundSpeed;
    [SerializeField]
    private float delayTime;

    void Start()
    {
        start();
    }
    void Update()
    {
        update();
        previousPosition = transform.position;
        if (!stopped)
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (delayTime <= 0 || Mathf.Abs(_speed) > 0.25f)
        {
            _speed = _speed - (_turnAroundSpeed * Time.deltaTime);
        }
        else
        {
            _speed = 0;
            delayTime = delayTime - Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _otherKnight)
        {
            SoundEffectsManager.instance.playSound(12, false);
            SoundEffectsManager.instance.playSound(5, false, 0.4f);
            GameObject sparks = Instantiate(hitSparks);
            Vector3 moveDirection = transform.position - previousPosition;
            sparks.transform.position = _otherKnight.transform.position + moveDirection/3;
        }
        else if (other.gameObject.tag == "Ground")
        {
            SoundEffectsManager.instance.playSound(11, false);
        }
        else if (other.gameObject.tag == "Weapon" && !collided)
        {
            SoundEffectsManager.instance.playSound(12, false);
            transform.Rotate(0, 0, 180);
            GameObject sparks = Instantiate(hitSparks);
            sparks.transform.position = transform.position;
            Camera.main.GetComponent<Shake>().startShake(0.15f, 0.15f);
        }
        onTriggerEnter2D(other);
    }
}

