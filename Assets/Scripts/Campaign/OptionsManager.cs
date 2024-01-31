using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Campaign
{
    public class OptionsManager : MonoSinglenton<OptionsManager>
    {
        [SerializeField] GameObject optionsPanel;
        [SerializeField] Button[] optionsButtons;

        private Action<int> onChoiceSelected;

        private void Start()
        {
            foreach (var option in optionsButtons)
            {
                option.onClick.AddListener(() => Clicked(option));
            }
        }

        public void ShowChoices(string[] options, Action<int> onChoiceSelected)
        {
            this.onChoiceSelected = onChoiceSelected;
            optionsPanel.SetActive(true);
            int i = 0;
            for (; i < options.Length; i++)
            {
                optionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];
                optionsButtons[i].gameObject.SetActive(true);
            }
            for (; i < optionsButtons.Length; i++)
                optionsButtons[i].gameObject.SetActive(false);
        }

        public void Clicked(Button button)
        {
            optionsPanel.SetActive(false);
            var index = optionsButtons.IndexOf(button);
            onChoiceSelected?.Invoke(index);
        }
    }
}