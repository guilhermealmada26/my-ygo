using BBG.Dueling.Selection;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Actions
{
    public abstract class ExtraSummon : CardAction
    {
        public Monster summoned;

        protected Player player;
        protected CardsSelection selection;
        protected List<Card> range;

        public delegate DuelAction ActionDelegate(ExtraSummon summon, Card target);
        public ActionDelegate GetDiscardAction;

        protected ExtraSummon(Monster summoned, Card caller, List<Card> range, ActionDelegate method) : base(summoned)
        {
            this.summoned = summoned;
            player = summoned.Controller;
            Caller = caller ?? summoned;
            this.range = range;
            range.Remove(summoned);
            GetDiscardAction = method;
            range.Filter(c => IsValid(c));
            selection = GetSelection();
            selection.CheckSelected = CardsAreValid;
            selection.MaxCardsSelected = MaxCardsSelected;
        }

        protected abstract bool IsValid(Card card);
        protected abstract CardsSelection GetSelection();
        protected abstract bool CardsAreValid(List<Card> cards);
        protected abstract bool MaxCardsSelected(List<Card> cards);

        private SpecialSummon GetSummonAction(List<Card> range)
        {
            var summon = new SpecialSummon(player, summoned, Caller)
            {
                reason = Reason.ExtraSummon
            };
            var materialsOnField = range.Count(c => c.Location == Location.MonsterZone);
            summon.cardsToRemoveBeforeSummon = materialsOnField;
            return summon;
        }

        protected override bool ValidPerform()
        {
            if (!selection.CanPerform() || !CardsAreValid(range) || !GetSummonAction(range).CanPerform())
                return false;
            return base.ValidPerform();
        }

        protected override void BeforePerform()
        {
            selection.Perform();
        }

        protected override void OnPerform()
        {
            if (selection.Cancelled)
                return;

            selection.Selected.ForEach(c => GetDiscardAction(this, c).Perform());
            GetSummonAction(selection.Selected).Perform();
            summoned.LastExtraSummon = this;
        }

        public List<Card> MaterialSelected => selection.Selected;
    }
}