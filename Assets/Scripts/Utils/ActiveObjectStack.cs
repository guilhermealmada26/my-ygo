using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectStack : MonoBehaviour
{
    [SerializeField] KeyCode disableLastKey = KeyCode.Escape;

    private Stack<GameObject> stack = new();

    public void SetActive(GameObject gameObject)
    {
        stack.Push(gameObject);
        gameObject.SetActive(true);
    }

    public void DisableLast()
    {
        var disabled = false;

        while(stack.Count > 0 && !disabled)
        {
            var last = stack.Pop();
            if(last.activeInHierarchy)
            {
                last.SetActive(false);
                disabled = true;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(disableLastKey))
        {
            DisableLast();
        }
    }
}
