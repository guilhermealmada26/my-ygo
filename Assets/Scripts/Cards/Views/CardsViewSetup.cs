using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo.CardViews
{
    public class CardsViewSetup : MonoBehaviour
    {
        [SerializeField] MultipleCardsView multipleCardsView;
        [SerializeField] FilterView filterView;

        private void Start()
        {
            var cards = new List<CardData>(CardDatabase.GetAllCards());
            multipleCardsView.SetCards(cards);
            filterView.Setup(cards);
        }
    }
}