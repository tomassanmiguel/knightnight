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

    public GameObject _otherKnight;
    private float _createTime;

    void Update()
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
            if (other.GetComponent<Combatant>().player == 1)
            {
                GameManager.instance.addP1Win();
            }
            else
            {
                GameManager.instance.addP2Win();
            }

            if (GameManager.instance.getOverallWinner() == 1)
            {
                Debug.Log("Player 1 Wins!");
            }
            else if (GameManager.instance.getOverallWinner() == 2)
            {
                Debug.Log("Player 2 Wins!");
            }

            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
}

