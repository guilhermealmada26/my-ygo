using BBG.OptionsSelection;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Dueling.Selection.View
{
    public class OptionSelectionHandler : ManualSelectionHandler<OptionSelection>
    {
        [SerializeField] Button confirmButton;
        [SerializeField] OptionSelectionUI optionSelectionUI;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            confirmButton.onClick.AddListener(Confirm);
            optionSelectionUI.SelectedChanged += () => confirmButton.interactable = true;
        }

        protected override void SelectionStarted(OptionSelection selection)
        {
            base.SelectionStarted(selection);
            confirmButton.interactable = false;
            optionSelectionUI.SetOptions(selection.Options);
        }

        public override void Confirm()
        {
            selection.choice = optionSelectionUI.SelectedIndex;
            base.Confirm();
        }
    }
}