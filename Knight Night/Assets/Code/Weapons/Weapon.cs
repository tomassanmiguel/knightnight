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
    private BoxCollider2D col;
    protected Vector3 previousPosition;
    public bool collided = false;

    public void update()
    {
        if (Time.time - _createTime > _timeToDestroy)
        {
            Destroy(gameObject);
        }

        if (stopped)
        {
            col.enabled = false;
        }
    }

    public void start()
    {
        col = GetComponent<BoxCollider2D>();
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
            g.GetComponent<Rigidbody2D>().mass = 8;
            g.GetComponent<Rigidbody2D>().gravityScale = 4.0f;
            g.GetComponent<Rigidbody2D>().AddForce(((transform.position + new Vector3(0, 0.2f, 0) - previousPosition) / (transform.position - previousPosition).magnitude) * 8000);
            if (transform.position.x > previousPosition.x)
                g.GetComponent<Rigidbody2D>().AddTorque(-800);
            else
                g.GetComponent<Rigidbody2D>().AddTorque(800);
            Camera.main.GetComponent<Shake>().startShake(1.0f, 0.3f);
            other.GetComponent<Combatant>().deadTimer = 1.2f;
            GameManager.instance.toDelete.Add(g);
            Time.timeScale = 0.3f;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            stopped = true;
        }

        if (other.gameObject.tag == "Weapon")
        {
            GameManager.instance.GetComponent<CrowdController>().increaseExcitement(1, 0.4f);
            GameManager.instance.GetComponent<CrowdController>().increaseExcitement(1, 0.4f);
            SoundEffectsManager.instance.playSound(4, false, 0.5f);
            collided = true;
        }
    }
}

