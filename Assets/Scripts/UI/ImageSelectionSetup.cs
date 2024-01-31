using BBG.ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(ImageSelection))]
public class ImageSelectionSetup : MonoBehaviour
{
    [SerializeField] SpriteCollection sprites;

    private void Start()
    {
        var imageSelection = GetComponent<ImageSelection>();
        imageSelection.SetImages(sprites.Assets);
    }
}
