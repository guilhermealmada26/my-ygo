using System.Collections.Generic;
using System.Linq;

namespace BBG.Ygo.DeckEditing
{
    [System.Serializable]
    public class DeckValues
    {
        public List<CardData> mallet;
        public List<Deck> decks = new();
        public Deck equipped;

        const int MAX_CARDS_COPIES = 3;

        public void AddToMallet(IEnumerable<CardData> cards)
        {
            cards.Foreach((c) => AddToMallet(c));
        }

        public void AddToMallet(CardData card)
        {
            var count = mallet.Count(c => c == card);
            if (count < MAX_CARDS_COPIES)
                mallet.Add(card);
        }

        public void AddDeckCardsToMallet()
        {
            mallet.Clear();
            decks.ForEach(d => AddDeckCardsToMallet(d));
        }

        public int EquippedIndex => decks.IndexOf(equipped);

        public void SetEquipped(int index) => equipped = decks[index];

        private void AddDeckCardsToMallet(Deck deck)
        {
            AddToMallet(deck.MainDeck);
            AddToMallet(deck.ExtraDeck);
        }
    }
}
