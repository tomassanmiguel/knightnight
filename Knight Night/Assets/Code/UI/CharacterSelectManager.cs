using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {

    //ScrollSelectors
    public ScrollSelector p1Selector;
    public ScrollSelector p2Selector;

    //Player Specific EventSystems
    public MultiEventSystem player1;
    public MultiEventSystem player2;

    //Character selects
    [SerializeField] private CharacterSelect p1Select;
    [SerializeField] private CharacterSelect p2Select;

    //Player Knight Choice
    [SerializeField] private PlayerChoiceData playerChoice;


    private void Awake()
    {
        player1.SetSelectedGameObject(p1Selector.gameObject);
        player2.SetSelectedGameObject(p2Selector.gameObject);
    }

    private void Update()
    {
        if (p1Select.ReadyCheck() && p2Select.ReadyCheck())
        {
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(false);
            p1Select.enabled = false;
            p2Select.enabled = false;
            playerChoice.player1 = p1Selector.GetSelectedKnight();
            playerChoice.player2 = p2Selector.GetSelectedKnight();
            LoadManager.instance.LoadScene("BattleScene");
        }
    }

    public void BackToMainMenu()
    {
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        p1Select.enabled = false;
        p2Select.enabled = false;
        LoadManager.instance.LoadScene("Menu");
    }
}
