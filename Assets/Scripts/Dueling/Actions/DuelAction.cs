using System;
using System.Collections.Generic;

namespace BBG.Dueling.Actions
{
    public abstract class DuelAction
    {
        public Priority priority;
        public Reason reason;
        internal bool blockTriggers;

        public bool Cancelled { private set; get; }
        public Card Caller { protected set; get; }
        public int TurnPerformed { private set; get; }
        internal readonly Dictionary<int, Action> beforePerformDelegates = new(2);
        public object CallerObject { internal set; get; }

        protected Duel duel => Duel.Current;

        public void Declare()
        {
            ActionDeclarer.Instance.Declare(this);
        }

        public bool CanPerform()
        {
            Events.TriggerValidate(this);
            return !Cancelled && ValidPerform();
        }

        protected virtual bool ValidPerform() => true;

        public void Perform()
        {
            SetData();
            if (!CanPerform())
                return;

            beforePerformDelegates.Foreach(e => e.Value.Invoke());
            BeforePerform();
            duel.PerformHandler.Invoke(this);
        }

        public void Perform(Priority _priority)
        {
            priority = _priority;
            Perform();
        }

        protected virtual void SetData() { }
        protected virtual void BeforePerform() { }

        internal void ProcessPerform()
        {
            if (!Cancelled)
            {
                TurnPerformed = duel.TurnCount;
                OnPerform();
                TurnActionsRegistry.Instance.Add(this);
            }
            if (!Cancelled) Events.TriggerAction(this);
        }

        protected virtual void OnPerform() { }

        public virtual void Cancel()
        {
            Cancelled = true;
        }
    }
}