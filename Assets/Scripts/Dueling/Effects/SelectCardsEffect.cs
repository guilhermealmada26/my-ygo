using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/SelectCardsEffect")]
    public class SelectCardsEffect : CardsEffect
    {
        internal override void Resolve(List<Card> cards)
        {
            //selection done in base class
        }
    }
}