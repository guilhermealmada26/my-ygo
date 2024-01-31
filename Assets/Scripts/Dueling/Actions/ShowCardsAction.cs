using System.Collections.Generic;

namespace BBG.Dueling.Actions
{
    public class ShowCardsAction : PlayerTargetAction
    {
        public readonly List<Card> cards;
        public bool IsEffect;
        public string message;

        public ShowCardsAction(List<Card> cards, Player player, bool waitConfirm = false) : base(player)
        {
            this.cards = cards;
            this.IsEffect = waitConfirm;
        }

        protected override bool ValidPerform()
        {
            if (cards.Count == 0)
                return false;
            return base.ValidPerform();
        }
    }
}