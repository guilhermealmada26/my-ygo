using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    [System.Serializable]
    public class DeckSerializedData
    {
        public string name;
        public string mainCardId;
        public string[] mainDeckIds;
        public string[] extraDeckIds;

        public DeckSerializedData(Deck deck)
        {
            name = deck.name;
            mainCardId = deck.MainCard == null ? string.Empty : deck.MainCard.ID;
            mainDeckIds = deck.MainDeck.GetIDs();
            extraDeckIds = deck.ExtraDeck.GetIDs();
        }

        public Deck GetNewDeck()
        {
            var deck = ScriptableObject.CreateInstance<Deck>();
            deck.name = name;
            deck.MainCard = CardDatabase.Get(mainCardId);
            mainDeckIds.Foreach(id => deck.Add(CardDatabase.Get(id), true));
            extraDeckIds.Foreach(id => deck.Add(CardDatabase.Get(id), true));
            return deck;
        }
    }
}