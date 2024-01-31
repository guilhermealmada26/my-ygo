using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo.CardViews
{
    public class MultipleCardsView : MonoBehaviour
    {
        [SerializeField] CardView prefab;
        [SerializeField] int cardsCount;
        [SerializeField] Transform cardsParent;
        [SerializeField] bool sortByDefault = true;

        private CardView[] cardViews;
        private List<CardData> cards;

        public CardComparer Comparer { set; get; } = new();
        public int CardsCount => cardsCount;

        protected virtual void Awake()
        {
            cardViews = new CardView[cardsCount];
            for (int i = 0; i < cardsCount; i++)
            {
                var view = Instantiate(prefab, cardsParent);
                cardViews[i] = view;
            }
        }

        public void SetCards(List<CardData> cards)
        {
            this.cards = cards;
            cards.Filter(c => c != null);
            if (sortByDefault)
                SortCards(null);
            else
                SetCardsView(cards);
        }

        public void SortCards(bool? reverseOrder)
        {
            if (reverseOrder.HasValue)
                Comparer.reverseOrder = reverseOrder.Value;
            Comparer.Sort(cards);
            SetCardsView(cards);
        }

        public void ResetCards()
        {
            if (cards != null)
                SetCards(cards);
        }

        protected virtual void SetCardsView(List<CardData> cards)
        {
            int length = Mathf.Clamp(cards.Count, 0, cardViews.Length);

            for (int i = 0; i < length; i++)
            {
                var view = cardViews[i];
                view.SetData(cards[i]);
                view.gameObject.SetActive(true);
            }
            for (int i = length; i < cardViews.Length; i++)
            {
                cardViews[i].gameObject.SetActive(false);
            }
        }
    }
}