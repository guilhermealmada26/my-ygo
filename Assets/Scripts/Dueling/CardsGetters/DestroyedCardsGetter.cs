using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.CardsGetters
{
    public class DestroyedCardsGetter : CardsGetter
    {
        public Reason[] reasons;
        public Condition[] callerFilter;

        public override List<Card> GetCards(Effect caller)
        {
            var cards = new List<Card>();
            var registry = TurnActionsRegistry.Instance;
            foreach (var destroy in registry.PerformedThisTurn<DestroyAction>())
            {
                if (reasons.Length > 0 && !reasons.Contains(destroy.reason))
                    continue;
                if (destroy.Caller != null && callerFilter.Length > 0 && !destroy.Caller.IsValid(caller, callerFilter))
                    continue;
                cards.AddNonContained(destroy.GetCards());
            }
            return cards;
        }
    }
}