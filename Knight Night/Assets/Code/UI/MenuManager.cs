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

    //Current Active Screen
    private GameObject currentScreen;

    private void Awake()
    {
        currentScreen = mainMenu;
        eventSystem.SetSelectedGameObject(startGameButton.gameObject);
    }

    public void StartGame()
    {
        currentScreen.SetActive(false);
        currentScreen = characterSelect;
        //Play animation
        currentScreen.SetActive(true);
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
