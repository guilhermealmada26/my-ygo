using BBG.Dueling;
using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Cards/DestroyedCondition")]
    public class DestroyedCondition : CardCondition
    {
        public bool selfWasDestroyed;
        public bool cardIsDestroyer;
        public Reason[] reasons;
        public Condition[] actionCardFilters, callerFilters;
        [Tooltip("If true: gets last move of the card. Otherwise it gets the trigger action of effect.")]
        public bool cardLastMove;

        protected override bool IsValid(Card card, Player player, Effect effect)
        {
            if (card == null || effect == null)
                return false;
            var action = cardLastMove ? card.LastMove : effect.TriggerAction;
            if (action == null || action is not DestroyAction destroy)
                return false;
            if (selfWasDestroyed && card != destroy.card)
                return false;
            if (cardIsDestroyer && card != destroy.Caller)
                return false;
            if (reasons.Length > 0 && !reasons.Contains(destroy.reason))
                return false;
            if (actionCardFilters.Length > 0 && !destroy.card.IsValid(effect, actionCardFilters))
                return false;
            if (destroy.Caller != null && callerFilters.Length > 0 && !destroy.Caller.IsValid(effect, callerFilters))
                return false;
            return true;
        }
    }
}