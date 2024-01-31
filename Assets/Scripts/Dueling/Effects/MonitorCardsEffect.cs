using BBG.Conditions;
using BBG.Dueling.Actions;
using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public class MonitorCardsEffect : CardsEffect
    {
        public CardsEffect[] effects;
        public bool showResolve;
        public EventName[] events;

        private bool active;

        protected override void OnSetup()
        {
            base.OnSetup();
            effects = effects.Clone(card);
        }

        internal override void BeforeRemove()
        {
            base.BeforeRemove();
            effects.Foreach(e => e.BeforeRemove());
        }

        protected override bool IsResolvable()
        {
            return !active;
        }

        internal override void Resolve(List<Card> cards)
        {
            effects.Foreach(e => e.Resolve(cards));
            events.Foreach(e => Events.Observe(e, OnActionPerformed));
            active = true;
        }

        protected override void OnRevert()
        {
            base.OnRevert();
            effects.Foreach(e => e.Revert());
            events.Foreach(e => Events.RemoveObserve(e, OnActionPerformed));
            active = false;
        }

        private void OnActionPerformed(DuelAction action)
        {
            if (action is not CardAction ca)
                return;
            if (!ca.card.IsValid(this, cardsFilter))
                return;

            if (showResolve)
                new ShowResolveAction(card).Perform();
            effects.Foreach(e => e.Resolve(ca.card));
        }
    }
}