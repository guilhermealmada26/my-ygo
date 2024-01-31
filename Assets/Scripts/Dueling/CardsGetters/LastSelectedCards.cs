using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/LastSelectedCards")]
    public class LastSelectedCards : CardsGetter
    {
        public override List<Card> GetCards(Effect caller)
        {
            var registry = Duel.Current.GetComponent<EffectRegistry>();
            var last = registry.LastSelection(caller.card);
            if (last != null)
            {
                return last;
            }

            return new List<Card>(0);
        }
    }
}