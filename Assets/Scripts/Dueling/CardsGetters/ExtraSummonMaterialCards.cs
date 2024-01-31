using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/ExtraSummonMaterialCards")]
    public class ExtraSummonMaterialCards : CardsGetter
    {
        public override List<Card> GetCards(Effect caller)
        {
            if (caller.card is not Monster monster || monster.LastExtraSummon == null)
                return new List<Card>(0);
            return new List<Card>(monster.LastExtraSummon.MaterialSelected);
        }
    }
}