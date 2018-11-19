using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour {

    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private GameObject defaultSelected;

    private bool paused = false;

    private void Update()
    {
        if (Input.GetButtonDown("AllMenu") && GameManager.instance.knightsReady)
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
                eventSystem.SetSelectedGameObject(defaultSelected);
                paused = true;
            }
        }
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void ReturnToMenu()
    {
        LoadManager.instance.LoadScene("Menu");
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
