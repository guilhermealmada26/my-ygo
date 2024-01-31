using BBG.Dueling.Actions;
using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public abstract class ActionModifier : Modifier
    {
        private bool subscribed;

        protected abstract EventName Event { get; }

        protected abstract void OnValidateAction(DuelAction action);

        protected override bool IsResolvable() => !subscribed;

        protected virtual void AddEvents(HashSet<EventName> events)
        {
            events.Add(Event);
        }

        protected void Validate(DuelAction action)
        {
            TriggerAction = action;
            if (!IsValid() || !IsValid(action))
                return;
            OnValidateAction(action);
        }

        protected virtual bool IsValid(DuelAction action) => true;

        protected override void OnResolve()
        {
            if (subscribed)
                return;
            //Debug.Log($"{IsValid()}/{card.data.cardName}");
            var events = new HashSet<EventName>();
            AddEvents(events);
            events.Foreach(e => Events.ObserveValidate(e, Validate));
            subscribed = true;
        }

        protected override void OnRevert()
        {
            if (!subscribed)
                return;
            var events = new HashSet<EventName>();
            AddEvents(events);
            events.Foreach(e => Events.RemoveValidate(e, Validate));
            subscribed = false;
            base.OnRevert();
        }
    }
}