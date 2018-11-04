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
        onTriggerEnter2D(other);
    }
}

