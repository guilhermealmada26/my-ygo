using System;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Selection
{
    public class CardsSelection : SelectionAction
    {
        public List<Card> range;
        public uint min, max;
        public bool setAllFaceUp;

        public List<Card> Selected { private set; get; }
        public Predicate<List<Card>> CheckSelected;
        public Predicate<List<Card>> MaxCardsSelected;

        public CardsSelection(Player player, List<Card> range, uint min, uint max = 0) : base(player)
        {
            this.range = range;
            this.min = min;
            if (max < min)
                max = min;
            this.max = max;
            Selected = new List<Card>((int)max);
            SetMessage();
        }

        public void SetMessage()
        {
            if (min == max)
                message = string.Format("Select {0} card(s).", min);
            else if (max < 100)
                message = string.Format("Select ({0}/{1}) card(s).", min, max);
            else
                message = string.Format("Select {0} or more card(s).", min);
        }

        int GetValue(Card card)
        {
            //card can have override value in this selection
            foreach (var eff in card.effects)
                if (eff is ICustomCardValue customValue)
                    return customValue.GetCount(this, card);
            return 1;
        }

        protected override bool ValidPerform()
        {
            if (range.Sum(c => GetValue(c)) < min)
                return false;
            return base.ValidPerform();
        }

        public override bool RequirementsAreDone()
        {
            var selectedCount = Selected.Sum(c => GetValue(c));
            if (selectedCount < min || selectedCount > max)
                return false;
            if (CheckSelected != null && !CheckSelected(Selected))
                return false;
            return base.RequirementsAreDone();
        }

        public bool MaxSelected()
        {
            if (MaxCardsSelected != null)
                return MaxCardsSelected(Selected);
            return Selected.Sum(c => GetValue(c)) >= max;
        }

        public bool AllCardsInHandOrField => range.All(c => c.Location.IsHandOrField());

        public override string GetLoadableData()
        {
            if (Cancelled)
                return string.Empty;
            return CardSerialization.GetIds(Selected);
        }

        protected override void SetLoadableData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                if (min > 0)
                    Cancel();
                else
                    Confirm();
            }
            else
                Selected = CardSerialization.GetCards(data);
        }
    }
}