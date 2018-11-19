using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Javelin : Weapon
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravity = 1.2f;
    private float vspeed = 0;

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

            transform.position = transform.position - new Vector3(0, vspeed * Time.deltaTime, 0);

            vspeed += _gravity;

            Vector3 moveDirection = transform.position - previousPosition;
            if (moveDirection != Vector3.zero)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _otherKnight)
        {
            SoundEffectsManager.instance.playSound(10, false);
            SoundEffectsManager.instance.playSound(5, false, 0.4f);
        }
        else if (other.gameObject.tag == "Ground")
        {
            SoundEffectsManager.instance.playSound(8, false);
        }
        else if (other.gameObject.tag == "Weapon" && !collided)
        {
            SoundEffectsManager.instance.playSound(10, false);
            transform.Rotate(0, 0, 180);
            Camera.main.GetComponent<Shake>().startShake(0.15f, 0.15f);
        }
        onTriggerEnter2D(other);
    }
}

