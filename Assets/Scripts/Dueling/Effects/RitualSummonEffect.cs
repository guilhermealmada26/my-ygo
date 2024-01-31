using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Dueling.CardsGetters;
using BBG.Ygo;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/RitualSummonEffect")]
    public class RitualSummonEffect : ExtraSummonEffect
    {
        public CardsGetter summonableGetter;
        public Condition[] summonableFilter;
        public CardData[] nameFilter;
        public CardsGetter materialsGetter;
        public LocationGetter location;

        protected override List<Card> GetSummonables()
        {
            var summonable = summonableGetter.GetCards(this);
            summonable.Filter(this, summonableFilter);
            summonable.Filter(c => c.data.type.subtype == Subtype.Ritual);
            if (nameFilter.Length > 0)
                summonable.Filter(c => nameFilter.Any((n) => n.cardName == c.data.cardName));
            summonable.Filter(c => GetSummonAction(c).CanPerform());
            return summonable;
        }

        protected override ExtraSummon GetSummonAction(Card toSummon)
        {
            var materials = materialsGetter.GetCards(this, toSummon);
            return new RitualSummon(toSummon as Monster, card, materials)
            {
                GetDiscardAction = (a, c) => new TributeAction(c, Reason.Effect, a.Caller) { destination = location.Get(c) }
            };
        }
    }
}