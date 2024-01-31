using BBG.Dueling.Selection;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Actions
{
    public class RitualSummon : ExtraSummon
    {
        public RitualSummon(Monster summoned, Card caller, List<Card> range) 
            : base(summoned, caller, range, DefaultAction)
        {
        }

        static DuelAction DefaultAction(ExtraSummon summon, Card target)
        {
            return new TributeAction(target, Reason.Effect, summon.Caller);
        }

        protected override CardsSelection GetSelection()
        {
            var message = "Select monsters until the level sum adds up to " + summoned.Lvl;
            return new CardsSelection(player, range, 1, 99)
            {
                message = message,
            };
        }

        protected override bool IsValid(Card card)
        {
            return card is Monster && new TributeAction(card, Reason.Effect, Caller).CanPerform();
        }

        protected override bool CardsAreValid(List<Card> cards)
        {
            //if the player's monster field is full, at least one valid material in range must be in monster zone
            if (player[Location.MonsterZone].Count >= player.FieldMaxCards)
            {
                var onField = cards.Count(c => c.Location == Location.MonsterZone);
                if(onField == 0)
                    return false;
            }

            var sum = cards.Sum(c => (c as Monster).Lvl);
            return sum >= summoned.Lvl;
        }

        protected override bool MaxCardsSelected(List<Card> cards) => CardsAreValid(cards);

        protected override void BeforePerform()
        {
            if (player[Location.MonsterZone].Count >= player.FieldMaxCards)
            {
                var onField = selection.range.Where(c => c.Location == Location.MonsterZone).ToList();
                var selectOnField = new CardsSelection(player, onField, 1);
                selectOnField.Perform();
                selectOnField.AfterSelection += (s) =>
                {
                    var selected = selectOnField.Selected[0];
                    selection.range.Remove(selected);
                    selection.Selected.Add(selected);
                    selection.Confirm();
                };
            }
            base.BeforePerform();
        }
    }
}