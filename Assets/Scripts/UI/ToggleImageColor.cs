using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleImageColor : MonoBehaviour
{
    [SerializeField] Color alternative;

    private Color original;
    private Image image;
    private bool changed;

    private void Awake()
    {
        image = GetComponent<Image>();
        original = image.color;
    }

    public void Switch()
    {
        Set(!changed);
    }

    public void Set(bool changed)
    {
        this.changed = changed;
        image.color = changed ? alternative : original;
    }
}
