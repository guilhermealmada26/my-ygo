using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    [CreateAssetMenu(menuName = "Cards/DeckStates/Empty")]
    public class DeckState : ScriptableObject
    {
        public virtual bool CanAddCard(CardData card, Deck deck)
        {
            return deck != null;
        }

        public virtual bool IsValid(Deck deck, List<CardData> mallet)
        {
            return deck != null;
        }
    }
}