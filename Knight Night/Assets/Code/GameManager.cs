using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // game state variables 
    public int numOfRounds = 0;

    private int currentRound = 0;

    public int p1Knight;
    public int p2Knight;

    public int p1Wins;
    public int p2Wins; 
    public List<int> winHistory = new List<int>(); 

    void Awake ()
    {
        if (GameManager.instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}

    // both increaseRound() and addP1Win() / addP2Win() need to be called
    public void increaseRound()
    {
        currentRound += 1; 
    }

    public void addP1Win()
    {
        p1Wins += 1;
        winHistory.Add(1);
        increaseRound();
    }

    public void addP2Win()
    {
        p2Wins += 1;
        winHistory.Add(2);
        increaseRound();
    }
	
    // get winner of a specific round 
    public int getWinner(int round)
    {
        return winHistory[round]; 
    }

    public int getOverallWinner()
    {
        if (p2Wins > p1Wins && p2Wins > numOfRounds / 2)
        {
            return 1;
        }
        else if (p1Wins > numOfRounds / 2)
        {
            return 2;
        }

        return 0;
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
