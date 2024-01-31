using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InvokeOnStart : MonoBehaviour
{
    public UnityEvent callback;
    public float delay = .2f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        callback.Invoke();
    }
}