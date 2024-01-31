using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    [CreateAssetMenu(menuName = "Cards/Deck")]
    public class Deck : ScriptableObject
    {
        [BoxGroup("Space", ShowLabel = false), PropertySpace(SpaceBefore = 8, SpaceAfter = 8)]
        [SerializeField] CardData mainCard;

        [SerializeField] List<CardData> mainDeck = new();

        [SerializeField] List<CardData> extraDeck = new();

        public CardData MainCard { get => mainCard; set => mainCard = value; }
        public List<CardData> MainDeck => mainDeck;
        public List<CardData> ExtraDeck => extraDeck;
        public int CardsCount => mainDeck.Count + extraDeck.Count;

        private List<CardData> List(CardData card) => card.GoesToExtraDeck ? extraDeck : mainDeck;

        public bool Add(CardData card, bool ignoreDeckRules = false)
        {
            if (!ignoreDeckRules && !DeckUtility.CanAddCard(card, this))
                return false;
            List(card).Add(card);
            return true;
        }

        public Deck Clone()
        {
            var clone = Instantiate(this);
            clone.name = name;
            return clone;
        }

        public bool Remove(CardData card)
        {
            return List(card).Remove(card);
        }

        public void RemoveAll()
        {
            mainDeck.Clear();
            extraDeck.Clear();
        }

        public int GetCount(CardData card) => List(card).GetCount(card);

        public bool Contains(CardData card) => GetCount(card) > 0;

        public bool IsValid(List<CardData> mallet)
        {
            return DeckUtility.IsValid(this, mallet);
        }
    }
}