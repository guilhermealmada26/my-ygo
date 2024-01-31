using BBG.Conditions;
using BBG.Dueling.Actions;
using System;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Modifiers/UnnaffectedByEffects")]
    public class UnnaffectedByEffects : Modifier
    {
        public EventName[] eventNames;
        public Location[] locations;
        public Condition[] callerFilter;

        protected override void OnResolve()
        {
            eventNames.Foreach(e => { Events.ObserveValidate(e, ActionValidate); });
        }

        protected override void OnRevert()
        {
            eventNames.Foreach(e => Events.RemoveValidate(e, ActionValidate));
        }

        internal bool Applies(Card caller)
        {
            if (!locations.Contains(card.Location) || !card.IsFaceUp())
                return false;
            var args = new FilterArgs(caller, this);
            if (!callerFilter.All(f => f.IsValid(args)))
                return false;
            return true;
        }

        private void ActionValidate(DuelAction action)
        {
            if (action is not CardAction cardAction)
                return;
            if (cardAction.Caller == null || cardAction.reason != Reason.Effect)
                return;
            if (cardAction.Caller == card || cardAction.card != card)
                return;
            if (!Applies(cardAction.Caller))
                return;

            action.Cancel();
        }
    }
}