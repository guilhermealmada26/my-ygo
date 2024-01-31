using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonsPopup : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] RectTransform buttonsParent;
    [SerializeField] Button[] buttons;
    [SerializeField] Sprite[] sprites;

    private int activeCount;

    public void AddAction(string name, Action action)
    {
        var button = buttons[activeCount];
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action.Invoke);
        button.onClick.AddListener(Hide);
        button.gameObject.SetActive(true);
        button.transform.GetChild(0).GetComponent<Image>().sprite = sprites.First(s => s.name == name);
        activeCount++;
    }

    public void ClearActions()
    {
        buttons.Foreach(b => b.gameObject.SetActive(false));
        activeCount = 0;
    }

    public void Show(Vector3 position, float scale, Action callback = null)
    {
        panel.SetActive(true);
        buttonsParent.position = position;
        buttonsParent.localScale = Vector3.one * scale;
        if (callback != null)
            buttons.Foreach(b => b.onClick.AddListener(callback.Invoke));
    }

    public void Hide() => panel.SetActive(false);
}