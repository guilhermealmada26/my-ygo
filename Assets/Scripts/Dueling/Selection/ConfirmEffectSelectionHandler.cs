using BBG.Dueling.View;
using BBG.Ygo.CardViews;
using UnityEngine;

namespace BBG.Dueling.Selection.View
{
    public class ConfirmEffectSelectionHandler : ManualSelectionHandler<ConfirmActivationSelection>
    {
        [SerializeField] CardView cardView;

        protected override void SelectionStarted(ConfirmActivationSelection selection)
        {
            base.SelectionStarted(selection);
            cardView.SetData(selection.effect.card.data);
            selection.effect.card.GetView().askActivate.SetActive(true);
        }

        protected override void SelectionEnded(ConfirmActivationSelection selection)
        {
            base.SelectionEnded(selection);
            selection.effect.card.GetView().askActivate.SetActive(false);
        }

        public void Choose(bool yes)
        {
            selection.choice = yes;
            Confirm();
        }
    }
}