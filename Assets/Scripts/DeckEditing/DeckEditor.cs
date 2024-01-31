using System;
using System.Collections.Generic;

namespace BBG.Ygo.DeckEditing
{
    public class DeckEditor
    {
        readonly List<CardData> mallet;
        public readonly Deck deck;

        public event Action CardsChanged;

        public List<CardData> MainDeck => deck.MainDeck;
        public List<CardData> ExtraDeck => deck.ExtraDeck;
        public List<CardData> Mallet => mallet;

        public DeckEditor(IEnumerable<CardData> _mallet, Deck deck)
        {
            mallet = new List<CardData>(_mallet);
            mallet.Sort(new CardComparer());
            this.deck = deck;
            deck.MainDeck.ForEach(c => mallet.Remove(c));
            deck.ExtraDeck.ForEach(c => mallet.Remove(c));
        }

        public bool AddCardToDeck(CardData card)
        {
            if (!mallet.Contains(card))
                return false;
            if (!deck.Add(card))
                return false;
            mallet.Remove(card);
            CardsChanged?.Invoke();
            return true;
        }

        public bool RemoveCardFromDeck(CardData card)
        {
            if (deck.Remove(card))
            {
                mallet.Add(card);
                CardsChanged?.Invoke();
                return true;
            }
            return false;
        }

        public void ClearDeck()
        {
            mallet.AddRange(deck.MainDeck);
            mallet.AddRange(deck.ExtraDeck);
            deck.RemoveAll();
            CardsChanged?.Invoke();
        }
    }
}
