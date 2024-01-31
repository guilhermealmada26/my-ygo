using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    [CreateAssetMenu(menuName = "Cards/DeckStates/Default")]
    public class DefaultDeckState : DeckState
    {
        [SerializeField] int minCardsMainDeck, maxCardsMainDeck, maxCardsExtraDeck;

        public override bool CanAddCard(CardData card, Deck deck)
        {
            if (!base.CanAddCard(card, deck))
                return false;
            if (card.GoesToExtraDeck && deck.ExtraDeck.Count + 1 >= maxCardsExtraDeck)
                return false;
            if (!card.GoesToExtraDeck && deck.MainDeck.Count + 1 >= maxCardsMainDeck)
                return false;
            var list = card.GoesToExtraDeck ? deck.ExtraDeck : deck.MainDeck;
            return LimitationIsValid(list, card, true);
        }

        private bool LimitationIsValid(IReadOnlyList<CardData> cards, CardData card, bool willAddCard)
        {
            int count = cards.Count(c => c.cardName == card.cardName);
            if (willAddCard)
                count++;
            return count <= card.limitation.maxCards;
        }

        private bool AllCardsLimitationAreValid(IReadOnlyList<CardData> cards)
        {
            var noDuplicates  = new HashSet<CardData>(cards);

            foreach (var card in noDuplicates)
            {
                if (!LimitationIsValid(cards, card, false))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool IsValid(Deck deck, List<CardData> mallet)
        {
            if (!base.IsValid(deck, mallet))
                return false;
            if (deck.MainDeck.Count < minCardsMainDeck)
                return false;
            if (deck.MainDeck.Count > maxCardsMainDeck)
                return false;
            if (deck.ExtraDeck.Count > maxCardsExtraDeck)
                return false;
            if (!AllCardsLimitationAreValid(deck.MainDeck))
                return false;
            if (!AllCardsLimitationAreValid(deck.ExtraDeck))
                return false;
            if(mallet != null)
            {
                //all cards in deck must be contained in mallet
                var cards = new List<CardData>(deck.MainDeck);
                cards.AddRange(deck.ExtraDeck);
                var cardsInBoth = cards.FindAll(x => mallet.Contains(x));
                return cardsInBoth.Count == deck.CardsCount;
            }
            return true;
        }
    }
}