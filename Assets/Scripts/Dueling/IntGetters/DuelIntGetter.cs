using BBG.Dueling;
using BBG.Dueling.Effects;

namespace BBG.ValueGetters
{
    public abstract class DuelIntGetter : IntGetter
    {
        public override int GetValue(object args)
        {
            if(args is Effect eff)
                return GetValue(eff.card, eff);
            if (args is FilterArgs filter)
                return GetValue(filter.target, filter.caller);
            if (args is Card card)
                return GetValue(card, null);
            return -1111;
        }

        protected abstract int GetValue(Card card, Effect caller);
    }
}