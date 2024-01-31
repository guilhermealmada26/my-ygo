using BBG.Dueling.Effects;
using System.Linq;

namespace BBG.Dueling.Actions
{
    public class EquipAction : CardAction, IRevertable
    {
        public readonly Card equip;

        private Player player => Caller.Controller;

        public EquipAction(Card equipped, Card equip, Card caller) : base(equipped)
        {
            this.equip = equip;
            reason = Reason.Other;
            Caller = caller;
        }

        protected override bool ValidPerform()
        {
            if (equip.Location != Location.STZone && player[Location.STZone].Count >= player.FieldMaxCards)
                return false;
            if (card.equips.Any(c => c.equip == equip))
                return false;
            return base.ValidPerform();
        }

        protected override void BeforePerform()
        {
            base.BeforePerform();
            if (equip.Location != Location.STZone)
            {
                new MoveCardAction(equip, Location.STZone) { playerTarget = player }.Perform();
                if (equip.Position != CardPosition.AtkFaceUp)
                    new DelegateAction(ChangePos).Perform();
            }
        }

        //delayed to wait for move action until modifiers are applied in validation
        private void ChangePos()
        {
            new ChangePositionAction(equip, CardPosition.AtkFaceUp).Perform();
        }

        protected override void OnPerform()
        {
            card.equips.Add(this);
            equip.equippedTo = this;
            card.location.ValueChanged += EquippedStateChanged;
            equip.location.ValueChanged += EquipStateChanged;
            foreach (var eff in equip.effects)
            {
                if (eff.type == Effect.Type.EquipEffect)
                    eff.Resolve();
            }
        }

        private void EquippedStateChanged()
        {
            if (!card.location.Previous.IsField() || card.Location == card.location.Previous)
                return;
            card.location.ValueChanged -= EquippedStateChanged;
            if (equip.Location == Location.STZone)
            {
                new DestroyAction(equip, Reason.EquipDiscard, card).Perform();
                new DelegateAction(Revert).Perform();
            }
        }

        private void EquipStateChanged()
        {
            new DelegateAction(Revert).Perform();
        }

        public void Revert()
        {
            if (card.equips.Remove(this))
            {
                card.location.ValueChanged -= EquippedStateChanged;
                equip.location.ValueChanged -= EquipStateChanged;
                Events.TriggerAction(this);
                equip.equippedTo = null;
                foreach (var eff in equip.effects)
                    if (eff.type == Effect.Type.EquipEffect)
                        eff.Revert();
            }
        }
    }
}