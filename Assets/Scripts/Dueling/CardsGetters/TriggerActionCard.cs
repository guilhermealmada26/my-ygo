using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/TriggerActionCard")]
    public class TriggerActionCard : CardsGetter
    {
        public bool callerCard;
        public bool getBoth;

        public override List<Card> GetCards(Effect caller)
        {
            var cards = new List<Card>(1);
            if (caller.TriggerAction == null || caller.TriggerAction is not ICardsHolder action)
                return cards;

            var triggerCaller = caller.TriggerAction.Caller;
            if (getBoth)
            {
                cards.Add(triggerCaller);
                cards.AddRange(action.GetCards());
            }
            else
            {
                cards.AddRange(callerCard ? new Card[] { triggerCaller } : action.GetCards());
            }

            cards.Filter(c => c != null);
            return cards;
        }
    }
}