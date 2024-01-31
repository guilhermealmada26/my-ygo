using UnityEngine;
using UnityEngine.UI;

namespace BBG.Dueling.Selection.View
{
    public class ScrollCardsSelectionHandler : CardsSelectionHandler
    {
        [SerializeField] CardSelectionView cardSelectionView;
        [SerializeField] int maxAmount = 50;
        [SerializeField] Scrollbar scrollbar;

        private CardSelectionView[] _views;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            _views = new CardSelectionView[maxAmount];
            _views[0] = cardSelectionView;
            for (int i = 1; i < _views.Length; i++)
            {
                _views[i] = Instantiate(cardSelectionView, cardSelectionView.transform.parent);
            }
        }

        protected override void SelectionStarted(CardsSelection selection)
        {
            base.SelectionStarted(selection);
            Invoke(nameof(UpdateScroll), 0.02f);
            for (int i = 0; i < _views.Length; i++)
                _views[i].gameObject.SetActive(i < selection.range.Count);
        }

        private void UpdateScroll()
        {
            scrollbar.value = 0;
        }

        protected override CardSelectionView[] GetSelectionViews(CardsSelection selection)
        {
            return _views;
        }

        protected override bool IsValid(CardsSelection selection)
        {
            return !selection.AllCardsInHandOrField;
        }

        protected override void OnCardClick(Card card, bool isSelected)
        {
            if (!isSelected && selection.MaxSelected())
            {
                if (selection.max == 1)
                {
                    selection.Selected.Clear();
                    selection.Selected.Add(card);
                    for (int i = 0; i < selection.range.Count; i++)
                        _views[i].UpdateSelectedView();
                }
                else
                    InvalidSfx();
            }
            else
            {
                if (!selection.Selected.Contains(card))
                    selection.Selected.Add(card);
                else
                    selection.Selected.Remove(card);
            }
        }
    }
}