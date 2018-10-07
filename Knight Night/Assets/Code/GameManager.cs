using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    // game state variables 
    public int numOfRounds = 0;


    // holding reference to players' selection 
    // steed, knight, weapon
    public Steed p1Steed, p2Steed;  // { get; set; } ??? 
    public Knight p1Knight, p2Knight;
    public Weapon p1Weapon, p2Weapon; 

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

    // idk if this is how i should do it
    // feed player select to GM 
    public void p1Feed(Steed s, Knight k, Weapon w)
    {
        p1Steed = s;
        p1Knight = k;
        p1Weapon = w; 
    }

    public void p2Feed(Steed s, Knight k, Weapon w)
    {
        p2Steed = s;
        p2Knight = k;
        p2Weapon = w;
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
    
    // Update is called once per frame
    void Update () {
		
	}
}
