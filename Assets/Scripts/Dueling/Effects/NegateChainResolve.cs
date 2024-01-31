using BBG.Conditions;
using BBG.Dueling.Actions;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/NegateChainResolve")]
    public class NegateChainResolve : SimpleEffect
    {
        public Condition[] cardFilter;
        public Effect afterNegate;

        private ResolveAction toNegate;

        protected override bool IsResolvable()
        {
            var lastResolve = duel.GetComponent<ChainSystem>().Last;
            if (lastResolve == null)
                return false;
            if (!cardFilter.All(f => f.IsValid(new FilterArgs(lastResolve.effect.card, this))))
                return false;
            return true;
        }

        public override void OnActivate()
        {
            base.OnActivate();
            toNegate = duel.GetComponent<ChainSystem>().Last;
        }

        protected override void OnResolve()
        {
            var card = toNegate.effect.card;
            new NegateAction(toNegate, card).Perform();
            if (afterNegate == null)
                return;

            if (afterNegate is CardsEffect cardsEffect)
                cardsEffect.Resolve(card);
            else
                afterNegate.Resolve();

            toNegate = null;
        }
    }
}