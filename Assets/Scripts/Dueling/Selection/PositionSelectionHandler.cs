using BBG.Ygo.CardViews;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Dueling.Selection.View
{
    public class PositionSelectionHandler : ManualSelectionHandler<PositionSelection>
    {
        [SerializeField] Button confirmButton;
        [SerializeField] CardView[] cardViews;

        private CardView selected;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            confirmButton.onClick.AddListener(Confirm);
            foreach (var cardView in cardViews)
                cardView.GetComponent<ClickEvent>().onClick.AddListener(() => CardClicked(cardView));
        }

        private void CardClicked(CardView cardView)
        {
            if(selected != null) 
                Higlight(selected).SetActive(false);
            selected = cardView;
            Higlight(selected).SetActive(true);
            confirmButton.interactable = true;
        }

        private GameObject Higlight(CardView card) => card.transform.GetChild(0).gameObject;

        protected override void SelectionStarted(PositionSelection selection)
        {
            base.SelectionStarted(selection);
            confirmButton.interactable = false;
            foreach(var cardView in cardViews)
            {
                cardView.SetData(selection.card.data.OriginalData);
                Higlight(cardView).SetActive(false);
            }
        }

        public override void Confirm()
        {
            var selectedIndex = cardViews.IndexOf(selected);
            selection.position = selection.availablePositions[selectedIndex];
            base.Confirm();
        }
    }
}