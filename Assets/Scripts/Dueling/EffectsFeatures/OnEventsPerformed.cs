using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public abstract class OnEventsPerformed : Feature
    {
        public EventName[] eventNames;

        internal override void OnResolve()
        {
            eventNames.Foreach(e => Events.Observe(e, ActionPerformed));
        }

        internal override void OnRevert()
        {
            eventNames.Foreach(e => Events.RemoveObserve(e, ActionPerformed));
        }

        protected abstract void ActionPerformed(DuelAction action);
    }
}