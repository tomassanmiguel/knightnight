using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Javelin : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timeToDestroy = 5;
    [SerializeField]
    private float _gravity = 1.2f;

    public GameObject _otherKnight;
    private float _createTime;
    private float vspeed = 0;
    private Vector3 previousPosition;
    private bool stopped = false;

    void Update()
    {
        if (!stopped)
        {
            previousPosition = transform.position;
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
        if (other.gameObject == _otherKnight && !stopped)
        {
            if (other.GetComponent<Combatant>().player == 1)
            {
                GameManager.instance.addP1Win();
            }
            else
            {
                GameManager.instance.addP2Win();
            }

            //Camera.main.GetComponent<CameraController>().StartSlowMo(0.2f, other.GetComponent<Combatant>().player);
            other.GetComponent<Rigidbody2D>().isKinematic = false;
            other.GetComponent<Rigidbody2D>().mass = 10;
            other.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            other.GetComponent<Rigidbody2D>().AddForce((transform.position + new Vector3(0,0.5f,0) - previousPosition)/(transform.position-previousPosition).magnitude * 6000);
            other.GetComponent<Combatant>().deadTimer = 1.0f;
            Time.timeScale = 0.3f;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            stopped = true;
        }
    }
}

