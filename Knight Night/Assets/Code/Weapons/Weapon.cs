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
            GameManager.instance.toDelete = g;
            Time.timeScale = 0.6f;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            stopped = true;
        }
    }
}

