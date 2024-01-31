using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.OptionsSelection
{
    public class OptionSelectionButton : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemText;
        [SerializeField] GameObject highlight;
        [SerializeField] Color normal, highlighted;
        [SerializeField] Image image;
        [SerializeField] Button button;

        public void SetData(Option data)
        {
            itemText.text = data.text;
            button.interactable = !data.isDisabled;
        }

        public void Highlight(bool value)
        {
            if (highlight != null)
                highlight.SetActive(value);
            image.color = value ? highlighted : normal;
        }
    }
}