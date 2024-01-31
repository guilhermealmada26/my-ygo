using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Dueling.CardsGetters;
using BBG.Dueling.Selection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    public class ReEquipEffect : CardsEffect
    {
        public CardsGetter equippableGetter;
        [Header("If none, the filter conditions of the equip card is considered (works for card with equip effect only).")]
        public Condition[] equippableFilter;
        public bool makeSelection;

        private EquipAction equipAction;

        internal override bool IsResolvable(Card equip)
        {
            List<Card> cards = GetEquippable(equip);
            return cards.Count > 0 && base.IsResolvable(card);
        }

        List<Card> GetEquippable(Card equip)
        {
            var cards = equippableGetter.GetCards(this);
            cards.Filter(this, equippableFilter);
            //check if the equipped can be selected by the equip card
            if (!equip.effects.Any(e => e is EquipCardEffect))
                return new List<Card>(0);
            var eff = equip.effects.First(e => e is EquipCardEffect) as CardsEffect;
            cards.Filter(eff, eff.cardsFilter);
            //cannot reequip to card already equipped with the given equip
            for (int i = cards.Count - 1; i >= 0; i--)
                if (equip.equippedTo != null && equip.equippedTo.card == cards[i])
                    cards.RemoveAt(i);
            return cards;
        }

        internal override void Resolve(List<Card> equips)
        {
            var equip = equips[0];
            var equipped = GetEquippable(equip);
            if (makeSelection)
            {
                var select = new CardsSelection(player, equipped, 1);
                select.Perform();
                select.AfterSelection += (s) => Reequip(equips, select.Selected[0]);
            }
            else
            {
                Reequip(equips, equipped[0]);
            }
        }

        void Reequip(List<Card> equips, Card equipped)
        {
            foreach (var equip in equips)
            {
                equip.equippedTo?.Revert();
                equipAction = new EquipAction(equipped, equip, card);
                equipAction.Perform();
            }
        }

        protected override void OnRevert()
        {
            base.OnRevert();
            equipAction?.Revert();
        }
    }
}