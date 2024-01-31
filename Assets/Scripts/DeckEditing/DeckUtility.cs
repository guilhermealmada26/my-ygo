using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    public class DeckUtility : MonoBehaviour
    {
        [SerializeField] DeckState currentDeckState;

        private static DeckUtility instance;

        private void Awake()
        {
            instance = this;
        }

        public static void SetData(Deck deckToSet, Deck deckToCopy)
        {
            deckToSet.name = deckToCopy.name;
            deckToSet.MainCard = deckToCopy.MainCard;
            deckToSet.RemoveAll();
            deckToSet.MainDeck.AddRange(deckToCopy.MainDeck);
            deckToSet.ExtraDeck.AddRange(deckToCopy.ExtraDeck);
        }

        internal static bool CanAddCard(CardData card, Deck deck)
        {
            return instance.currentDeckState.CanAddCard(card, deck);
        }

        internal static bool IsValid(Deck deck, List<CardData> mallet)
        {
            return instance.currentDeckState.IsValid(deck, mallet); 
        }
    }
}