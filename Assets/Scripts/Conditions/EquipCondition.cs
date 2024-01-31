using BBG.Dueling;
using BBG.Dueling.Effects;

namespace BBG.Conditions
{
    public class EquipCondition : CardCondition
    {
        protected override bool IsValid(Card card, Player player, Effect effect)
        {
            if (effect.card.equippedTo == null)
                return false;
            return effect.card.equippedTo.card == card;
        }
    }
}