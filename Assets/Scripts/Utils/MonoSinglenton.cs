using UnityEngine;

public abstract class MonoSinglenton<T> : MonoBehaviour where T : MonoSinglenton<T>
{
    public static T Instance { private set; get; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        Instance = null;
    }
}
