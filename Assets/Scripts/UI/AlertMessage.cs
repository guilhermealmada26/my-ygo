using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AlertMessage : MonoBehaviour
{
    [SerializeField] GameObject messagePanel = null;
    [SerializeField] TextMeshProUGUI messageText = null;
    [SerializeField] KeyCode[] closeKeys = new KeyCode[0];

    [Header("Confirm panel")]
    [SerializeField] GameObject confirmPanel = null;
    [SerializeField] TextMeshProUGUI confirmText = null;

    public UnityEvent OnActive;
    public UnityEvent OnDisable;
    public UnityEvent OnClickOk;
    public UnityEvent OnSelectYes;
    public UnityEvent OnSelectNo;

    private Action _onSelectYes;
    private Action _onSelectNo;

    void Start()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas)
        {
            canvas.worldCamera = Camera.main;
        }
        messagePanel.SetActive(false);
        OnActive.AddListener(delegate ()
        {
            EventSystem.current.SetSelectedGameObject(null);
        });
    }

    void Update()
    {
        var closeKeyPressed = closeKeys.Any(k => Input.GetKeyDown(k));
        if (closeKeyPressed && messagePanel.activeSelf)
        {
            MessageConfirm();
        }
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        messagePanel.SetActive(true);
        OnActive.Invoke();
    }

    void MessageConfirm()
    {
        OnClickOk.Invoke();
        messagePanel.SetActive(false);
        OnDisable.Invoke();
    }

    public void Confirm(string message)
    {
        confirmText.text = message;
        confirmPanel.SetActive(true);
        OnActive.Invoke();
    }

    public void Confirm(string message, Action onConfirm, Action onCancel = null)
    {
        _onSelectYes = onConfirm;
        _onSelectNo = onCancel;
        Confirm(message);
        OnActive.Invoke();
    }

    public void Yes()
    {
        confirmPanel.SetActive(false);
        OnDisable.Invoke();

        OnSelectYes.Invoke();
        _onSelectYes?.Invoke();
        _onSelectYes = null;
        _onSelectNo = null;
    }

    public void No()
    {
        confirmPanel.SetActive(false);
        OnDisable.Invoke();

        OnSelectNo.Invoke();
        _onSelectNo?.Invoke();
        _onSelectYes = null;
        _onSelectNo = null;
    }

    public void TurnOff()
    {
        messagePanel.SetActive(false);
        confirmPanel.SetActive(false);
        OnDisable.Invoke();
    }

    public bool IsSelecting => confirmPanel.activeSelf || messagePanel.activeSelf;
}
