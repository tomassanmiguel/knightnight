using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectManager : MonoBehaviour {

    //ScrollSelectors
    public ScrollSelector p1Selector;
    public ScrollSelector p2Selector;
    public static PlayerChoiceData knightChoice;

    //Character selects
    [SerializeField] private CharacterSelect p1Select;
    [SerializeField] private CharacterSelect p2Select;

    //Player Knight Choice
    [SerializeField] private PlayerChoiceData playerChoice;
    

    private void Update()
    {
        if (p1Select.ReadyCheck() && p2Select.ReadyCheck())
        {
            p1Select.enabled = false;
            p2Select.enabled = false;
            playerChoice.player1 = p1Selector.GetSelectedKnight();
            playerChoice.player2 = p2Selector.GetSelectedKnight();
            CharacterSelectManager.knightChoice = playerChoice;
            LoadManager.instance.LoadScene("BattleScene");
        }
    }

    public void BackToMainMenu()
    {
        p1Select.enabled = false;
        p2Select.enabled = false;
        LoadManager.instance.LoadScene("Menu");
    }
}
