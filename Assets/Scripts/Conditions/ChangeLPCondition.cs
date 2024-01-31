using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using BBG.Dueling.PlayersGetters;
using System.Linq;

namespace BBG.Conditions
{
    public class ChangeLPCondition : Condition
    {
        public PlayersGetter target;
        public Condition[] callerFilter;
        public bool isDamage;
        public Reason[] validReasons;

        public override bool IsValid(object args)
        {
            if (args is not Effect effect)
                return false;
            var action = effect.TriggerAction;
            if (action == null || action is not ChangeLPAction changeLP)
                return false;
            if (!target.IsValid(effect, changeLP.player))
                return false;
            if (validReasons.Length > 0 && !validReasons.Contains(changeLP.Reason))
                return false;
            if (isDamage && changeLP.amount < 0)
                return false;
            if (!isDamage && changeLP.amount > 0)
                return false;
            if (changeLP.Caller != null && !changeLP.Caller.IsValid(effect, callerFilter))
                return false;
            return true;
        }
    }
}