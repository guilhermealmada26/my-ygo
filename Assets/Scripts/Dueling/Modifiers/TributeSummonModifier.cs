using BBG.Dueling.Actions;
using BBG.ValueGetters;
using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public class TributeSummonModifier : ActionModifier
    {
        public bool isOptional;
        public bool includeNormalSummon;
        public bool includeSetSummon;
        public IntGetter tributeAmount;

        protected override EventName Event => EventName.NormalSummon;

        protected override void AddEvents(HashSet<EventName> events)
        {
            if (includeNormalSummon)
                events.Add(EventName.NormalSummon);
            if (includeSetSummon)
                events.Add(EventName.SetSummon);
        }

        protected override bool IsValid(DuelAction action)
        {
            var summon = action as NormalSummon;
            if (summon.card != card)
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var summon = action as NormalSummon;
            if (isOptional)
                summon.TributeAmounts.AddNonContained(new int[] { tributeAmount.GetValue(this) });
            else
                summon.TributeAmounts[0] = tributeAmount.GetValue(this);
        }
    }
}