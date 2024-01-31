using BBG.Dueling.Actions;
using System;

namespace BBG.Dueling.Selection
{
    public abstract class SelectionAction : PlayerTargetAction
    {
        public bool isOptional;
        public string message;
        public bool Confirmed { private set; get; }

        public event Action<SelectionAction> AfterSelection;

        protected SelectionAction(Player player) : base(player) { }

        public bool IsFinished()
        {
            if (isOptional && Cancelled)
                return true;
            return Confirmed;
        }

        public virtual bool RequirementsAreDone()
        {
            return true;
        }

        public override void Cancel()
        {
            base.Cancel();
            AfterSelection?.Invoke(this);
        }

        public void Confirm()
        {
            if (RequirementsAreDone())
            {
                Confirmed = true;
                AfterSelection?.Invoke(this);
            }
        }

        public void Confirm(string selectionData)
        {
            SetLoadableData(selectionData);
            Confirm();
        }

        protected override void OnPerform()
        {
            Events.TriggerAction(this, nameof(SelectionAction));
        }

        public abstract string GetLoadableData();

        protected abstract void SetLoadableData(string data);
    }
}