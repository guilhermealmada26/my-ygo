using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/NormalSummonTributedCards")]
    public class NormalSummonTributedCards : CardsGetter
    {
        public override List<Card> GetCards(Effect caller)
        {
            if (caller.card is not Monster monster || monster.LastSummon == null)
                return new List<Card>(0);
            if (monster.LastSummon is not NormalSummon ns || ns.Tributed == null)
                return new List<Card>(0);

            return new List<Card>(ns.Tributed);
        }
    }
}