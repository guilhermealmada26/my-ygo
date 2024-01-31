using BBG.Dueling.View;

namespace BBG.Dueling.Selection.View
{
    public class FieldCardsSelectionHandler : CardsSelectionHandler
    {
        protected override CardSelectionView[] GetSelectionViews(CardsSelection selection)
        {
            var selectionViews = new CardSelectionView[selection.range.Count];
            for (int i = 0; i < selection.range.Count; i++)
            {
                var view = selection.range[i].GetView();
                selectionViews[i] = view.GetComponent<CardSelectionView>();
            }
            return selectionViews;
        }

        protected override bool IsValid(CardsSelection selection)
        {
            return selection.AllCardsInHandOrField;
        }

        protected override void OnCardClick(Card card, bool isSelected)
        {
            if (!isSelected && selection.MaxSelected())
            {
                InvalidSfx();
                return;
            }

            if (!selection.Selected.Contains(card))
            {
                selection.Selected.Add(card);
                if (selection.max == 1)
                    selection.Confirm();
            }
            else
            {
                selection.Selected.Remove(card);
            }
        }
    }
}