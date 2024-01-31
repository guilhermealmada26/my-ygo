using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using System.Linq;

namespace BBG.Conditions
{
    public class ActionReasonCondition : Condition
    {
        public Reason[] reasons;

        public override bool IsValid(object args)
        {
            var eff = args as Effect;
            if (args is FilterArgs fa)
                eff = fa.caller;
            if (eff == null)
                return false;
            var action = eff.TriggerAction;
            if (action == null)
                return false;
            return reasons.Contains(action.reason);
        }
    }
}