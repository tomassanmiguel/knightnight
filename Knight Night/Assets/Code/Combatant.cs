using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Combatant : MonoBehaviour {

    [SerializeField]
    public string jumpButton;
    [SerializeField]
    public string throwButton;
    [SerializeField]
    public string xAxisAim;
    [SerializeField]
    public string yAxisAim;
    [SerializeField]
    public Vector2 sortingOrders;
    [SerializeField]
    public GameObject opposingKnight;
    [SerializeField]
    public int player;
    [SerializeField]
    public bool facingLeft;
    [SerializeField]
    public Vector2 positionBounds;
    [SerializeField]
    public Vector2 hitBoxXOffsets;

    public float deadTimer = 0;

    public bool rtt;

    void Update()
    {
        if (deadTimer > 0)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer <= 0)
            {
                GameManager.instance.resetScene();
            }
        }
    }

}
