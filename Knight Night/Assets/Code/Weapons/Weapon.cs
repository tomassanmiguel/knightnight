using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Weapon : MonoBehaviour
{
    private float _createTime;
    public GameObject _otherKnight;
    [SerializeField]
    private float _timeToDestroy = 5;

    public bool stopped = false;
    protected Vector3 previousPosition;

    public void update()
    {
        if (Time.time - _createTime > _timeToDestroy)
        {
            Destroy(gameObject);
        }

    }

    public void start()
    {
        _createTime = Time.time;
    }

    public void onTriggerEnter2D(Collider2D other)
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
            other.GetComponent<Rigidbody2D>().AddForce((transform.position + new Vector3(0, 0.5f, 0) - previousPosition) / (transform.position - previousPosition).magnitude * 6000);
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

