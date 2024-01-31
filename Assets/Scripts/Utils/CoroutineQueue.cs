using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQueue : MonoBehaviour
{
    private Queue<IEnumerator> coroutines;
    private bool paused;

    public static CoroutineQueue Instance { private set; get; }
    public bool Executing { get; private set; }

    private void Awake()
    {
        Instance = this;
        coroutines = new Queue<IEnumerator>();
    }

    public void Pause(bool value)
    {
        paused = Executing = value;

        if (paused)
        {
            StopCoroutine(Execute());
        }
        else
        {
            StartCoroutine(Execute());
        }
    }

    public void AddCoroutine(IEnumerator coroutine)
    {
        coroutines.Enqueue(coroutine);
        if (!Executing)
        {
            Executing = true;
            StartCoroutine(Execute());
        }
    }

    private IEnumerator Execute()
    {
        while (coroutines.Count > 0 && !paused)
        {
            yield return coroutines.Dequeue();
        }

        Executing = false;
    }

    /// <summary>
    /// Class used together with Coroutine Queue, and yield until the there are coroutines executing.
    /// </summary>
    public class WaitCoroutinesEnd : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                if (Instance == null)
                    return false;

                return Instance.Executing;
            }
        }
    }
}