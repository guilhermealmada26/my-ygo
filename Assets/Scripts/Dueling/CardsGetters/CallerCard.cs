using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/CallerCard")]
    public class CallerCard : CardsGetter
    {
        public override List<Card> GetCards(Effect caller)
        {
            return new List<Card>(1) { caller.card };
        }
    }
}