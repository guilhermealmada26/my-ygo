using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionProcessor : MonoBehaviour
{   
    private class PAction
    {
        public int priority;
        public Action action;
    }

    private List<PAction> actions = new();
    public static ActionProcessor Instance { private set; get; }

    public bool Processing { private set; get; }
    public event Action OnSequenceStart, OnSequenceEnd;
    public Func<bool> Wait { set; get; } = () => false;

    void Awake()
    {
        Instance = this;
    }

    public void Process(Action action, int priority = 0)
    {
        actions.Add(new PAction{ action = action, priority = priority });
        actions = actions.OrderByDescending((a) => a.priority).ToList();
        if (!Processing){
            StartCoroutine(ProcessActions());
        }
    }

    IEnumerator ProcessActions()
    {
        Processing = true;
        OnSequenceStart?.Invoke();
        yield return null;  //wait for sorting the first actions added

        while (actions.Count > 0)
        {
            var action = actions[0];
            actions.RemoveAt(0);
            action.action?.Invoke();
            yield return new WaitWhile(Wait);
        }

        OnSequenceEnd?.Invoke();
        Processing = false;
    }
}
