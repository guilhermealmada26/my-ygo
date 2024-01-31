using System;

namespace BBG.Dueling.Actions
{
    public class DelegateAction : DuelAction
    {
        private Action action;

        public DelegateAction(Action action)
        {
            this.action = action;
        }

        protected override void OnPerform()
        {
            action.Invoke();
        }
    }
}