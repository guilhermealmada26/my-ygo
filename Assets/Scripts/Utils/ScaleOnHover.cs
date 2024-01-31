using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float onHoverFactor;

    private Vector3 originalScale;

    private void Start() { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enabled && originalScale == Vector3.zero)
        {
            originalScale = transform.localScale;
            transform.localScale *= onHoverFactor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (enabled && originalScale != Vector3.zero)
        {
            transform.localScale = originalScale;
            originalScale = Vector3.zero;
        }
    }
}