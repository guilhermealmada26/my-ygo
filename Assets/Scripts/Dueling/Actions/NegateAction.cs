using UnityEngine;

namespace BBG.Dueling.Actions
{
    public class NegateAction : DuelAction
    {
        public DuelAction action;
        public Card card;

        public NegateAction(DuelAction action, Card card)
        {
            this.action = action;
            this.card = card;
        }

        protected override void OnPerform()
        {
            action.Cancel();
        }
    }
}