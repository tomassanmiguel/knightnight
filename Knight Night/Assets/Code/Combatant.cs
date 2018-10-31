using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Combatant : MonoBehaviour {

    [SerializeField]
    public KeyCode jumpButton;
    [SerializeField]
    public KeyCode throwButton;
    [SerializeField]
    public string xAxisAim;
    [SerializeField]
    public string yAxisAim;
    [SerializeField]
    public KeyCode aimUpButton;
    [SerializeField]
    public KeyCode aimDownButton;
    [SerializeField]
    public KeyCode aimRightButton;
    [SerializeField]
    public KeyCode aimLeftButton;
    [SerializeField]
    public Vector2 sortingOrders;
    [SerializeField]
    public GameObject opposingKnight;
    [SerializeField]
    public int player;

    public float deadTimer = 0;

    public bool rtt;

    void Update()
    {
        if (deadTimer > 0)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer <= 0)
            {
                Time.timeScale = 1.0f;
                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
    }

}
