using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/LastResolveCard")]
    public class LastResolveCard : CardsGetter
    {
        public override List<Card> GetCards(Effect caller)
        {
            var system = Duel.Current.GetComponent<ChainSystem>();
            if(system.Last == null || system.Last.card == caller.card)
                return new List<Card>(0);
            return new List<Card>(1) { system.Last.card };
        }
    }
}