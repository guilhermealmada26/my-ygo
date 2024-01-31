using UnityEngine;
using UnityEngine.UI;

namespace BBG.Dueling.Selection.View
{
    public abstract class CardsSelectionHandler : ManualSelectionHandler<CardsSelection>
    {
        [SerializeField] Button confirm, cancel;
        [SerializeField] AudioClip invalidSfx;

        private CardSelectionView[] views;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            confirm.onClick.AddListener(Confirm);
            cancel.onClick.AddListener(Cancel);
        }

        protected override void SelectionStarted(CardsSelection selection)
        {
            base.SelectionStarted(selection);
            cancel.gameObject.SetActive(selection.isOptional);
            confirm.interactable = selection.RequirementsAreDone();
            views = GetSelectionViews(selection);
            for (int i = 0; i < selection.range.Count; i++)
            {
                views[i].SetData(selection.range[i], selection, CardClicked);
            }
        }

        protected override void SelectionEnded(CardsSelection selection)
        {
            base.SelectionEnded(selection);
            for (int i = 0; i < selection.range.Count; i++)
            {
                views[i].Disable();
            }
        }

        protected abstract CardSelectionView[] GetSelectionViews(CardsSelection selection);

        private void CardClicked(Card card, bool isSelected)
        {
            OnCardClick(card, isSelected);
            confirm.interactable = selection.RequirementsAreDone();
        }

        protected abstract void OnCardClick(Card card, bool isSelected);

        protected void InvalidSfx()
        {
            if (selection.min > 0)
                SoundManager.Instance.PlaySfx(invalidSfx);
        }
    }
}