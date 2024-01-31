using BBG.Dueling;
using BBG.Dueling.Effects;

namespace BBG.Conditions
{
    public abstract class CardCondition : Condition
    {
        public override bool IsValid(object args)
        {
            if (args is FilterArgs filter)
                return IsValid(filter.target, filter.caller.player, filter.caller);
            if (args is Card card)
                return IsValid(card, card.Controller, null);
            if (args is Effect effect)
                return IsValid(effect.card, effect.player, effect);
            return false;
        }

        protected abstract bool IsValid(Card card, Player player, Effect effect);
    }
}