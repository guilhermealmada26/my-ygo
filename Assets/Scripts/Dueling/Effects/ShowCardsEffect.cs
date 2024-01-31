using BBG.Dueling.Selection;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/ShowCardsEffect")]
    public class ShowCardsEffect : CardsEffect
    {
        public string message;

        internal override void Resolve(List<Card> cards)
        {
            new CardsSelection(player, cards, 0, 0) { message = message, setAllFaceUp = true }.Perform();
        }
    }
}