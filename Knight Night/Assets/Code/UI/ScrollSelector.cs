using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollSelector : Selectable {

    public float cycleTime;

    public int eventSystemIndex;


    private string[] names = new string[1];
    private int currIndex;

    //References
    private Text nameText;
    private MultiEventSystem eventSystem;

    //internal use
    private bool selected = false;
    private bool noSpam = false;


    protected override void Awake()
    {
        base.Awake();
        nameText = transform.Find("Name Box").GetComponentInChildren<Text>();
        nameText.text = names[currIndex];
        eventSystem = MultiEventSystem.GetMultiEventSystem(eventSystemIndex);
    }

    protected override void Start()
    {
        base.Start();
        //grab array of names from GameManager
    }

    public void setNames(string[] nm)
    {
        names = nm;
        nameText.text = names[currIndex];
    }

    public int getSelection()
    {
        return currIndex;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        selected = true;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        selected = false;
    }

    public override void Select()
    {
        if (eventSystem.alreadySelecting)
            return;
        eventSystem.SetSelectedGameObject(gameObject);
    }

    private void checkIndexBounds()
    {
        if(currIndex < 0)
        {
            currIndex = names.Length - 1;
        }
        else if (currIndex >= names.Length)
        {
            currIndex = 0;
        }
    }

    private void SpamDelayed()
    {
        noSpam = false;
    }

    private void Update()
    {
        if (selected && !noSpam)
        {
            if(Input.GetAxis("Horizontal") > 0)
            {
                //Right
                currIndex++;
                checkIndexBounds();
                nameText.text = names[currIndex];
                noSpam = true;
                Invoke("SpamDelayed", cycleTime);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                //Left
                currIndex--;
                checkIndexBounds();
                nameText.text = names[currIndex];
                noSpam = true;
                Invoke("SpamDelayed", cycleTime);
            }
        }

        //DEBUG
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            string[] temp = { "A", "B", "C", "D" };
            setNames(temp);
        }
    }
}
