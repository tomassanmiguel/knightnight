using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    // game state variables 
    public int numOfRounds = 0;

    // holding a reference to each knight 
    // public knight1 = steed, knight, weapon
    // public knight2 = steed, knight, weapon 

    public int p1Wins = 0;
    public int p2Wins = 0; 
    public List<int> winHistory = new List<int>(); 

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}

    // both increaseRound() and addP1Win() / addP2Win() need to be called
    public void increaseRound()
    {
        numOfRounds += 1; 
    }

    public void addP1Win()
    {
        p1Wins += 1;
        winHistory.Add(1); 
    }

    public void addP2Win()
    {
        p2Wins += 1;
        winHistory.Add(2); 
    }
	
    // get winner of a specific round 
    public int getWinner(int round)
    {
        return winHistory[round]; 
    }

    public int getOverallWinner()
    {
        if (p2Wins > p1Wins)
        {
            return 1;
        }
        return 2; 
    }

    /* returning knights' steeds/weapons?  what return type
     * public Combatant getP1()
     * {
     * 
     * }
     * 
     * 
     * 
     * public Combatant getP2() 
     * {
     * 
     * }
     * 
    */

	// Update is called once per frame
	void Update () {
		
	}
}
