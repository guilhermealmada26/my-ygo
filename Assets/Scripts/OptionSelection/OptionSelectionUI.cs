using System;
using UnityEngine;

namespace BBG.OptionsSelection
{
    public class OptionSelectionUI : MonoBehaviour
    {
        [SerializeField] OptionSelectionButton prefabButton;
        [SerializeField] int maxOptions;

        private OptionSelectionButton[] buttons;

        public int SelectedIndex { private set; get; }
        public event Action SelectedChanged;

        private void Start()
        {
            buttons = new OptionSelectionButton[maxOptions];
            buttons[0] = prefabButton;
            for (int i = 1; i < maxOptions; i++)
                buttons[i] = Instantiate(prefabButton, prefabButton.transform.parent);
        }

        public void SetOptions(params Option[] options)
        {
            SelectedIndex = 0;

            for (int i = 0; i < options.Length; i++)
            {
                buttons[i].SetData(options[i]);
                buttons[i].gameObject.SetActive(true);
                buttons[i].Highlight(false);
            }
            for (int i = options.Length; i < maxOptions; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

        public void SetSelectedIndex(OptionSelectionButton button)
        {
            buttons[SelectedIndex].Highlight(false);
            SelectedIndex = buttons.IndexOf(button);
            buttons[SelectedIndex].Highlight(true);
            SelectedChanged?.Invoke();
        }
    }
}