using BBG.Dueling.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/ReturnCard")]
    public class ReturnCardEffect : CardsEffect
    {
        [Tooltip("Either Deck or Hand")]
        public bool isDeck;
        public bool shuffle;

        internal override void Resolve(List<Card> cards)
        {
            var location = isDeck ? Location.Deck : Location.Hand;
            foreach (var target in cards)
                new MoveCardAction(target, location, card).Perform();
            if (shuffle)
                new ShuffleAction(cards[0].Controller, location).Perform();
        }
    }
}