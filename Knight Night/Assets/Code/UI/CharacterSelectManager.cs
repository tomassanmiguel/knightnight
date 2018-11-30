using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectManager : MonoBehaviour {

    //ScrollSelectors
    public ScrollSelector p1Selector;
    public ScrollSelector p2Selector;
    public Image p1Image;
    public Image p2Image;
    public static PlayerChoiceData knightChoice;

    //Character selects
    [SerializeField] private CharacterSelect p1Select;
    [SerializeField] private CharacterSelect p2Select;

    //Player Knight Choice
    [SerializeField] private PlayerChoiceData playerChoice;
    

    private void Update()
    {
        p1Image.sprite = p1Selector.GetSelectedKnight().CharSelectImage;
        p2Image.sprite = p2Selector.GetSelectedKnight().CharSelectImage;
        if (p1Select.ReadyCheck() && p2Select.ReadyCheck())
        {
            p1Select.enabled = false;
            p2Select.enabled = false;
            playerChoice.player1 = p1Selector.GetSelectedKnight();
            playerChoice.player2 = p2Selector.GetSelectedKnight();
            CharacterSelectManager.knightChoice = playerChoice;
            LoadManager.instance.LoadScene("Versus");
        }
        if (Input.GetButtonDown("AllMenu"))
        {
            BackToMainMenu();
        }
    }

    public void BackToMainMenu()
    {
        p1Select.enabled = false;
        p2Select.enabled = false;
        LoadManager.instance.LoadScene("Menu");
    }
}
