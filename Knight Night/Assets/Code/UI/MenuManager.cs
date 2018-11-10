using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.PostProcessing;

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

    //TransitionScreen
    [SerializeField] private Animator transitionScreen;

    //Current Active Screen
    private GameObject currentScreen;

    //Current active gameobject
    private GameObject lastSelected;

    //currently running transition
    private Coroutine currTransition;

    //Character selects
    [SerializeField] private CharacterSelect p1Select;
    [SerializeField] private CharacterSelect p2Select;

    //Player Knight Choice
    [SerializeField] private PlayerChoiceData playerChoice;

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
            if(p1Select.ReadyCheck() && p2Select.ReadyCheck())
            {
                playerChoice.player1 = p1Selector.GetSelectedKnight();
                playerChoice.player2 = p2Selector.GetSelectedKnight();
                LoadManager.instance.LoadScene("BattleScene");
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
        if(currTransition == null)
        {
            currTransition = StartCoroutine(transitionToControls());
        }
    }

    private IEnumerator transitionToControls()
    {
        eventSystem.gameObject.SetActive(false);
        transitionScreen.gameObject.SetActive(true);
        transitionScreen.SetBool("active", true);
        yield return new WaitForSeconds(0.5f);
        Camera.main.GetComponent<PostProcessingBehaviour>().enabled = false;
        currentScreen.SetActive(false);
        currentScreen = controls;
        currentScreen.SetActive(true);
        controls.GetComponent<Animator>().SetBool("active", true);
        yield return new WaitForSeconds(0.5f);
        currTransition = null;

    }

    private IEnumerator transitionFromControls()
    {
        controls.GetComponent<Animator>().SetBool("active", false);
        yield return new WaitForSeconds(0.5f);
        currentScreen.SetActive(false);
        Camera.main.GetComponent<PostProcessingBehaviour>().enabled = true;
        currentScreen = mainMenu;
        currentScreen.SetActive(true);
        transitionScreen.SetBool("active", false);
        yield return new WaitForSeconds(0.5f);
        eventSystem.gameObject.SetActive(true);
        currTransition = null;
    }

    public void ToCredits()
    {
        if (currTransition == null)
        {
            currTransition = StartCoroutine(transitionToCredits());
        }
    }

    private IEnumerator transitionToCredits()
    {
        eventSystem.gameObject.SetActive(false);
        transitionScreen.gameObject.SetActive(true);
        transitionScreen.SetBool("active", true);
        yield return new WaitForSeconds(0.5f);
        Camera.main.GetComponent<PostProcessingBehaviour>().enabled = false;
        currentScreen.SetActive(false);
        currentScreen = credits;
        currentScreen.SetActive(true);
        credits.GetComponent<Animator>().SetBool("active", true);
        yield return new WaitForSeconds(0.5f);
        currTransition = null;

    }

    private IEnumerator transitionFromCredits()
    {
        credits.GetComponent<Animator>().SetBool("active", false);
        yield return new WaitForSeconds(0.5f);
        currentScreen.SetActive(false);
        Camera.main.GetComponent<PostProcessingBehaviour>().enabled = true;
        currentScreen = mainMenu;
        currentScreen.SetActive(true);
        transitionScreen.SetBool("active", false);
        yield return new WaitForSeconds(0.5f);
        eventSystem.gameObject.SetActive(true);
        currTransition = null;
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
        else if(currentScreen == controls)
        {
            if(currTransition == null)
            {
                currTransition = StartCoroutine(transitionFromControls());
            }
        }
        else if(currentScreen == credits)
        {
            if (currTransition == null)
            {
                currTransition = StartCoroutine(transitionFromCredits());
            }
        }
        else
        {
            currentScreen.SetActive(false);
            currentScreen = mainMenu;
            //Play animation
            currentScreen.SetActive(true);
        }
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
