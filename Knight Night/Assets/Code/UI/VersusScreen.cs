using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersusScreen : MonoBehaviour {

    [SerializeField] private PlayerChoiceData choices;

    [SerializeField] private Image p1Img;
    [SerializeField] private Image p2Img;

    [SerializeField] private Text p1Name;
    [SerializeField] private Text p2Name;

    [SerializeField] private Animator p1;
    [SerializeField] private Animator p2;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Animator versusText;
    [SerializeField] private Camera main;

    [SerializeField] private float minWaitTime;

    private SoundEffectsManager sfx;


    private void Awake()
    {
        p1Img.sprite = choices.player1.CharSelectImage;
        p2Img.sprite = choices.player2.CharSelectImage;

        p1Name.text = choices.player1.KnightName;
        p2Name.text = choices.player2.KnightName;

        sfx = GetComponent<SoundEffectsManager>();

        StartCoroutine(BeginBattle());
    }

    private IEnumerator BeginBattle()
    {
        while (p1.GetCurrentAnimatorStateInfo(0).IsName("VersusEnter") && p1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        AsyncOperation op = SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
        op.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(minWaitTime);
        while (op.progress < 0.9f)
        {
            yield return null;
        }
        op.allowSceneActivation = true;
        p1.SetTrigger("Exit");
        p2.SetTrigger("Exit");
        versusText.enabled = true;
        yield return new WaitForEndOfFrame();
        main.GetComponent<AudioListener>().enabled = false;
        while (p1.GetCurrentAnimatorStateInfo(0).IsName("VersusExit") && p1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync("Versus");
    }

}
