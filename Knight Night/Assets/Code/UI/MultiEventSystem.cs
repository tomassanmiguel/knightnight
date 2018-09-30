using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiEventSystem : EventSystem {

    private static List<MultiEventSystem> multiEventSystemList = new List<MultiEventSystem>();

    private int index = -1;

    protected override void Awake()
    {
        base.Awake();

        multiEventSystemList.Add(this);
        index = multiEventSystemList.IndexOf(this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        EventSystem originalCurrent = EventSystem.current;
        current = this;
        base.Update();
        current = originalCurrent;
    }

    public static MultiEventSystem GetMultiEventSystem(int i)
    {
        return multiEventSystemList[i];
    }
}
