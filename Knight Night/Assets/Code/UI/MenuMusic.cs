using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnLevelWasLoaded(int level)
    {
        if(LoadManager.GetCurrentSceneName() != "Menu" && LoadManager.GetCurrentSceneName() != "CharacterSelect")
        {
            Destroy(gameObject);
        }
    }
}
