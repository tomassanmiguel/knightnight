using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour {

    [SerializeField]
    public bool _facingLeft;
    [SerializeField]
    public Vector2 _positionBounds;
    [SerializeField]
    public float _ridingSpeed;
    [SerializeField]
    public float _jumpForce;
    [SerializeField]
    public float _gravity;
    [SerializeField]
    public float _throwForce;
    [SerializeField]
    public KeyCode _jumpButton;
    [SerializeField]
    public KeyCode _throwButton;
    [SerializeField]
    public string _xAxisAim;
    [SerializeField]
    public string _yAxisAim;
    [SerializeField]
    public Vector2 _sortingOrders;
    [SerializeField]
    public GameObject _opposingKnight;
    [SerializeField]
    public GameObject _javelin;

    public Steed steed;
    public Knight knight;
    public Weapon weapon;

    public bool hasJavelin;

    void Awake()
    {
        steed = (Steed)(new DJ());
        knight = (Knight)(new SirLance());
        weapon = (Weapon)(new Javelin());
    }


    void Start()
    {
        System.Type type = steed.GetType();
        gameObject.AddComponent(type);

        type = knight.GetType();
        gameObject.AddComponent(type);

        hasJavelin = true;
    }

}
