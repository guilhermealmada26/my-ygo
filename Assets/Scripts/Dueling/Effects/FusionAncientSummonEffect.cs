using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Dueling.CardsGetters;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/FusionAncientSummon")]
    public class FusionAncientSummonEffect : ExtraSummonEffect
    {
        public Condition[] summonableFilter;
        public CardsGetter materialsGetter, obrigatoryCards;
        public LocationGetter location;

        protected override List<Card> GetSummonables()
        {
            //there must be at least 1 summonable that can be extra summoned          
            var summonable = new List<Card>(player[Location.ExtraDeck]);
            summonable.Filter(this, summonableFilter);
            summonable.Filter(c => GetSummonAction(c).CanPerform());
            return summonable;
        }

        protected override ExtraSummon GetSummonAction(Card toSummon)
        {
            var materials = materialsGetter.GetCards(this, toSummon);
            materials.Filter(m => IsValid(m, toSummon));
            var action = new AncientFusionSummon(toSummon as Monster, card, materials, false)
            {
                GetDiscardAction = (a, c) => new MaterialDiscard(c, a.summoned, a.Caller) { destination = location.Get(c) }
            };
            if (obrigatoryCards != null)
                action.ObrigatoryMaterials = obrigatoryCards.GetCards(this, toSummon).ToArray();
            return action;
        }

        private bool IsValid(Card material, Card toSummon)
        {
            //cannot fuse with opponent face down cards
            if (material.Controller != toSummon.Controller)
                return material.IsFaceUp();
            return true;
        }
    }
}