using UnityEngine;

namespace BBG.Ygo.CardViews
{
    public class CardTipManager : MonoSinglenton<CardTipManager>
    {
        [SerializeField] CardTip mainTooltip, detailsTooltip;

        private CardData currentCard;

        public void ShowOnMainTooltip(CardData card)
        {
            mainTooltip.SetCard(card);
            currentCard = card;
        }

        public void UpdateStats()
        {
            if (currentCard != null)
                mainTooltip.SetCard(currentCard);
        }

        public void ShowDetails()
        {
            detailsTooltip.SetCard(currentCard.OriginalData);
        }
    }
}