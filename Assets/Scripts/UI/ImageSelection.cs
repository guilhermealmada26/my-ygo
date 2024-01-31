using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelection : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image image;
    [SerializeField] int selectedIndex;

    public Sprite Selected => sprites[selectedIndex];
    public event Action SelectedChanged;

    public void SetImages(Sprite[] sprites, int selectedIndex = 0)
    {
        this.sprites = sprites;
        SetSelected(selectedIndex);
    }

    public void SetSelected(string name)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == name)
            {
                SetSelected(i);
                return;
            }
        }
    }

    public void SetSelected(int selectedIndex)
    {
        if(selectedIndex < 0) 
            selectedIndex = sprites.Length - 1;
        else if(selectedIndex >= sprites.Length)
            selectedIndex = 0;

        this.selectedIndex = selectedIndex;
        image.sprite = sprites[selectedIndex];
        SelectedChanged?.Invoke();
    }

    public void Next() => SetSelected(selectedIndex + 1);

    public void Previous() => SetSelected(selectedIndex - 1);
}
