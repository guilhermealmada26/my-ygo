using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject view;

    public void OnPointerEnter(PointerEventData eventData)
    {
        view.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        view.SetActive(false);
    }
}