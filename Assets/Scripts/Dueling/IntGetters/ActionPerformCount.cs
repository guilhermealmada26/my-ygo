using BBG.Dueling;

namespace BBG.ValueGetters
{
    public class ActionPerformCount : IntGetter
    {
        public bool afterResolveOnly;
        public EventName eventName;

        public override int GetValue(object args)
        {
            var actions = TurnActionsRegistry.Instance.PerformedThisTurn(eventName);
            return actions.Count;
        }
    }
}