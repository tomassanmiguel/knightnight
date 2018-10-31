using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {

    [SerializeField] private MenuManager menuManager;
   
    [SerializeField] private PlayerInputData input;
    [SerializeField] private GameObject readyOverlay;

    [SerializeField] private ScrollSelector selector;

    private bool ready = false;

    public bool ReadyCheck()
    {
        return ready;
    }

    private void Update()
    {
        if (Input.GetButtonDown(input.Fire1))
        {
            ready = true;
            readyOverlay.SetActive(true);
            selector.SetActive(false);
        }
        if (ready)
        {
            if (Input.GetButtonDown(input.Fire2))
            {
                ready = false;
                readyOverlay.SetActive(false);
                selector.SetActive(true);
            }
        }
        else
        {
            if (Input.GetButtonDown(input.Fire2))
            {
                menuManager.BackToMainMenu();
            }
        }
    }
}
