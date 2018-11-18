using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // game state variables 
    public int numOfRounds = 0;

    private int currentRound = 0;

    public ScoreIndicator p1ScoreIndicator;
    public ScoreIndicator p2ScoreIndicator;

    public Transform p1Spawn;
    public Transform p2Spawn;

    public Image p1Portrait;
    public Image p2Portrait;

    public Text p1Text;
    public Text p2Text;

    public GameObject toDelete = null;

    public PlayerInputData p1inputdata;
    public PlayerInputData p2inputdata;

    public AnnouncementGenerator ag;

    public KnightData p1Knight;
    public KnightData p2Knight;

    private GameObject p1;
    private GameObject p2;

    public bool knightsReady;

    public int p1Wins;
    public int p2Wins; 
    public List<int> winHistory = new List<int>(); 

    void Awake ()
    {
        knightsReady = false;
        if (GameManager.instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        //Non-Persistant Now!
        //DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {

	}

    public void setKnightData(KnightData p1, KnightData p2)
    {
        p1Knight = p1;
        p2Knight = p2;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Set Knight UI Stuff

        //Instantiate the Knights
        instantiateKnights();

        p1Text.text = p1Knight.KnightName;
        p2Text.text = p2Knight.KnightName;

        //Set Portraits

        startBattle();
    }

    // both increaseRound() and addP1Win() / addP2Win() need to be called
    public void increaseRound()
    {
        currentRound += 1; 
    }

    public void instantiateKnights()
    {
        p1 = Instantiate(p1Knight.Prefab);
        p2 = Instantiate(p2Knight.Prefab);

        Combatant c1 = p1.GetComponent<Combatant>();
        Combatant c2 = p2.GetComponent<Combatant>();

        c1.opposingKnight = p2;
        c2.opposingKnight = p1;

        c1.jumpButton = p1inputdata.Fire1;
        c2.jumpButton = p2inputdata.Fire1;
        c1.throwButton = p1inputdata.Fire2;
        c2.throwButton = p2inputdata.Fire2;
        c1.xAxisAim = p1inputdata.Horizontal;
        c2.xAxisAim = p2inputdata.Horizontal;
        c1.yAxisAim = p1inputdata.Vertical;
        c2.yAxisAim = p2inputdata.Vertical;
        c1.positionBounds = new Vector2(p1Spawn.position.x, p2Spawn.position.x);
        c2.positionBounds = new Vector2(p1Spawn.position.x, p2Spawn.position.x);
        c2.facingLeft = true;
        c1.player = 1;
        c2.player = 2;
        p1.transform.position = p1Spawn.position;
        p2.transform.position = p2Spawn.position;
    }

    public void resetScene()
    {
        Destroy(p1);
        Destroy(p2);
        if (toDelete != null)
            Destroy(toDelete);

        knightsReady = false;

        instantiateKnights();
        Time.timeScale = 1;
        startBattle();
    }

    public void startBattle()
    {
        ag.Announce("Ready?", 1);
        ag.Announce("3", 0.5f);
        ag.Announce("2", 0.5f);
        ag.Announce("1", 0.5f);
        ag.Announce("Fight!", 0.75f, true, 0.5f, 2);
        Invoke("unlock", 4.0f);
    }

    void unlock()
    {
        knightsReady = true;
    }

    public void addP1Win()
    {
        p1Wins += 1;
        winHistory.Add(1);
        p1ScoreIndicator.ShowScore(p1Wins);
        increaseRound();
    }

    public void addP2Win()
    {
        p2Wins += 1;
        winHistory.Add(2);
        p2ScoreIndicator.ShowScore(p2Wins);
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
}
