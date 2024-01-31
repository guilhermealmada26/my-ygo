using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public class CannotChangePosition : CardActionModifier
    {
        protected override EventName Event => EventName.ChangePositionAction;

        protected override void AddEvents(HashSet<EventName> events)
        {
            base.AddEvents(events);
            events.Add(EventName.FlipPositionAction);
        }
    }
}