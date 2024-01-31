using BBG.Dueling.Effects;

namespace BBG.Conditions
{
    public class CallerObjectCondition : Condition
    {
        public bool thisEffect;
        public bool thisEffectCard;

        public override bool IsValid(object args)
        {
            if (args is not Effect effect)
                return false;
            if (thisEffect)
                return effect.TriggerAction.CallerObject == args;
            if (thisEffectCard)
            {
                var eff = effect.TriggerAction.CallerObject as Effect;
                if (eff == null)
                    return false;
                return eff.card == effect.card;
            }
            return false;
        }
    }
}