using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour {

    public static LoadManager instance;

    public float transitionFadeTime;
    private Image blackScreen;

    private Coroutine loading;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        blackScreen = GetComponentInChildren<Image>();
        Debug.Log(blackScreen);
    }

    private void Start()
    {
        blackScreen.canvasRenderer.SetAlpha(0f);
        blackScreen.enabled = true;
    }

    //DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene("BattleScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
        }
    }

    //Scene loading functions

    public void LoadScene(string sceneName)
    {
        if(loading == null)
        {
            loading = StartCoroutine(TransitionToScene(sceneName));
        }
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        Time.timeScale = 0;
        yield return StartCoroutine(FadeOut(transitionFadeTime));
        SceneManager.LoadSceneAsync(sceneName);
        yield return StartCoroutine(FadeIn(transitionFadeTime));
        Time.timeScale = 1;
        yield return null;
        loading = null;
    }

    //Transition functions

    private IEnumerator FadeOut(float time)
    {
        if (blackScreen.canvas.worldCamera == null) blackScreen.canvas.worldCamera = Camera.main;
        float idx = 0f;
        float increment = 1 / (time / 0.01f);
        while (blackScreen.canvasRenderer.GetAlpha() < 1f)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            blackScreen.canvasRenderer.SetAlpha(idx);
            idx += increment;
        }
    }

    private IEnumerator FadeIn(float time)
    {
        if (blackScreen.canvas.worldCamera == null) blackScreen.canvas.worldCamera = Camera.main;
        float idx = 1f;
        float increment = 1 / (time / 0.01f);
        while (blackScreen.canvasRenderer.GetAlpha() > 0f)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            blackScreen.canvasRenderer.SetAlpha(idx);
            idx -= increment;
        }
    }



    //Static functions for getting scene information

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
