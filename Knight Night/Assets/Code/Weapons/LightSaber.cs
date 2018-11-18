using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightSaber : Weapon
{
    [SerializeField]
    private float _speed;

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

        Vector3 moveDirection = transform.position - previousPosition;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //onTriggerEnter2D(other); Lightsaber bounces!
        if (other.gameObject.tag == "Ground")
        {
            Vector3 moveDirection = transform.position - previousPosition;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(360 - angle, Vector3.forward);
            //Works because we bounce only off the ground!
        }

        if (other.gameObject == _otherKnight && !stopped)
        {
            if (other.GetComponent<Combatant>().player == 1)
            {
                GameManager.instance.addP2Win();
                GameManager.instance.GetComponent<CrowdController>().increaseExcitement(2, 0.8f);
            }
            else
            {
                GameManager.instance.addP1Win();
                GameManager.instance.GetComponent<CrowdController>().increaseExcitement(1, 0.8f);
            }

            //Camera.main.GetComponent<CameraController>().StartSlowMo(0.2f, other.GetComponent<Combatant>().player);
            GameObject g = other.GetComponent<Knight>().body;
            g.transform.parent = null;
            other.GetComponent<BoxCollider2D>().enabled = false;
            g.GetComponent<BoxCollider2D>().enabled = true;
            g.GetComponent<Rigidbody2D>().isKinematic = false;
            g.GetComponent<Rigidbody2D>().mass = 10;
            g.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            g.GetComponent<Rigidbody2D>().AddForce((transform.position + new Vector3(0, 0.5f, 0) - previousPosition) / (transform.position - previousPosition).magnitude * 8000);
            other.GetComponent<Combatant>().deadTimer = 1.0f;
            Camera.main.GetComponent<Shake>().startShake(0.35f, 1f);
            GameManager.instance.toDelete = g;
            Time.timeScale = 0.6f;
            Destroy(gameObject);
        }
    }
}

