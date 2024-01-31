using BBG.Dueling.Effects;
using BBG.Dueling;
using BBG.Ygo;

namespace BBG.Conditions
{
    public abstract class CardDataCondition : Condition
    {
        public override bool IsValid(object args)
        {
            if (args is FilterArgs filter)
                return IsValid(filter.target.data);
            if (args is Card card)
                return IsValid(card.data);
            if (args is CardData data)
                return IsValid(data);
            return false;
        }

        protected abstract bool IsValid(CardData data);
    }
}