using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour {

    public static MenuMusic instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
<<<<<<< HEAD
        if(scene.name != "Menu" && scene.name != "CharacterSelect")
=======
        if(LoadManager.GetCurrentSceneName() != "Menu" && LoadManager.GetCurrentSceneName() != "CharacterSelect")
>>>>>>> 3f411032de8fbc60623830a767a36da12ae6a70d
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
}
