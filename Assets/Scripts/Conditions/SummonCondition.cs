using BBG.Dueling;
using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using System.Linq;

namespace BBG.Conditions
{
    public class SummonCondition : CardCondition
    {
        public bool lastSummon;
        public bool thisTurn;
        public EventName[] validSummons;
        public Condition[] callerFilter;

        protected override bool IsValid(Card card, Player player, Effect effect)
        {
            if (card is not Monster monster)
                return false;
            var action = lastSummon ? monster.LastSummon : effect.TriggerAction;
            if (action is not SummonAction summon)
                return false;
            if (!validSummons.Any(s => s.ToString() == summon.GetType().Name))
                return false;
            if (thisTurn && summon.TurnPerformed != Duel.Current.TurnCount)
                return false;
            if (callerFilter.Length > 0 && summon.Caller != null && !summon.Caller.IsValid(effect, callerFilter))
                return false;
            return true;
        }
    }
}