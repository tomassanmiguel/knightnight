﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ScrollSelector : Selectable {

    public float cycleTime;

    public int eventSystemIndex;

    public KnightCollection knightCollection;

    [SerializeField] private IntUnityEvent changeSelection;

    [SerializeField]
    private PlayerInputData input;
    
    private int currIndex;

    //References
    private Text nameText;
    private MultiEventSystem eventSystem;

    //internal use
    private bool selected = false;
    private bool noSpam = false;
    private bool active = true;


    protected override void Awake()
    {
        base.Awake();
        nameText = transform.Find("Name Box").GetComponentInChildren<Text>();
        nameText.text = knightCollection.Collection[currIndex].KnightName;
        changeSelection.Invoke(currIndex);
        eventSystem = MultiEventSystem.GetMultiEventSystem(eventSystemIndex);

        if (changeSelection == null) changeSelection = new IntUnityEvent();
    }

    protected override void Start()
    {
        base.Start();
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
            currIndex = knightCollection.Collection.Length - 1;
        }
        else if (currIndex >= knightCollection.Collection.Length)
        {
            currIndex = 0;
        }
    }

    private IEnumerator SpamDelayed()
    {
        yield return new WaitForSeconds(cycleTime);
        noSpam = false;
    }

    public void SetActive(bool a)
    {
        active = a;
    }

    public KnightData GetSelectedKnight()
    {
        return knightCollection.Collection[currIndex];
    }

    private void Update()
    {
        if (selected && active && !noSpam)
        {
            if(Input.GetAxis(input.Horizontal) > 0)
            {
                //Right
                currIndex++;
                checkIndexBounds();
                nameText.text = knightCollection.Collection[currIndex].KnightName;
                changeSelection.Invoke(currIndex);
                noSpam = true;
                StartCoroutine(SpamDelayed());
            }
            else if (Input.GetAxis(input.Horizontal) < 0)
            {
                //Left
                currIndex--;
                checkIndexBounds();
                nameText.text = knightCollection.Collection[currIndex].KnightName;
                changeSelection.Invoke(currIndex);
                noSpam = true;
                StartCoroutine(SpamDelayed());
            }
        }
    }
}