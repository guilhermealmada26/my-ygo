using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public class EquipCardEffect : CardsActionEffect
    {
        public bool callerIsEquipped;

        protected override DuelAction GetAction(Card target)
        {
            var equipped = callerIsEquipped ? card : target;
            var equip = callerIsEquipped ? target : card;
            return new EquipAction(equipped, equip, card);
        }
    }
}