using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

    //Screens
    public GameObject mainMenu;
    public GameObject characterSelect;
    public GameObject controls;
    public GameObject credits;

    //Main menu buttons
    public Button startGameButton;
    public Button controlsButton;
    public Button creditsButton;
    public Button quitButton;

    //Default EventSystem
    public EventSystem eventSystem;

    //Player Specific EventSystems
    public MultiEventSystem player1;
    public MultiEventSystem player2;

    //ScrollSelectors
    public ScrollSelector p1Selector;
    public ScrollSelector p2Selector;

    //Current Active Screen
    private GameObject currentScreen;

    //Current active gameobject
    private GameObject lastSelected;

    private void Awake()
    {
        currentScreen = mainMenu;
        eventSystem.SetSelectedGameObject(startGameButton.gameObject);
        lastSelected = startGameButton.gameObject;
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    

    private void Update()
    {
        if(currentScreen == mainMenu)
        {
            if(eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(lastSelected);
            }
            else
            {
                lastSelected = eventSystem.currentSelectedGameObject;
            }
        }
        else if(currentScreen == characterSelect)
        {
            if(Input.GetButtonDown("P1Fire2") || Input.GetButtonDown("P2Fire2"))
            {
                BackToMainMenu();
            }
        }
        else
        {
            if (Input.GetButtonDown("P1Fire2"))
            {
                BackToMainMenu();
            }
        }
    }

    public void StartGame()
    {
        currentScreen.SetActive(false);
        currentScreen = characterSelect;
        //Play animation
        currentScreen.SetActive(true);

        eventSystem.gameObject.SetActive(false);
        //Player 1
        player1.gameObject.SetActive(true);
        player1.SetSelectedGameObject(p1Selector.gameObject);
        //Player 2
    }

    public void ToControls()
    {
        currentScreen.SetActive(false);
        currentScreen = controls;
        //Play animation
        currentScreen.SetActive(true);
    }

    public void ToCredits()
    {
        currentScreen.SetActive(false);
        currentScreen = credits;
        //Play animation
        currentScreen.SetActive(true);
    }

    public void BackToMainMenu()
    {
        if(currentScreen == characterSelect)
        {
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(false);
            eventSystem.gameObject.SetActive(true);
            eventSystem.SetSelectedGameObject(startGameButton.gameObject);
        }
        Debug.Log(currentScreen);
        currentScreen.SetActive(false);
        currentScreen = mainMenu;
        //Play animation
        currentScreen.SetActive(true);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
