using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Vector2 sortingOrders;
    [SerializeField]
    public GameObject opposingKnight;
    [SerializeField]
    public int player;

    public bool rtt;
}
