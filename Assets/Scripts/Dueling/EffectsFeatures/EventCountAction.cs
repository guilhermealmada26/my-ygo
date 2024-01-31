using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public abstract class EventCountAction : Feature
    {
        public EventName eventName;
        public int required = 1;

        private int count;

        internal override void OnResolve()
        {
            count = 0;
            Events.Observe(eventName, OnPerform);
        }

        private void OnPerform(DuelAction action)
        {
            count++;
            if (count == required)
            {
                Events.RemoveObserve(eventName, OnPerform);
                OnCountReached();
            }
        }

        protected abstract void OnCountReached();
    }
}