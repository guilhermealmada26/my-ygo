using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public class CardActionUnrespondable : CardActionModifier
    {
        public EventName eventName;

        protected override EventName Event => eventName;

        protected override void OnValidateAction(DuelAction action)
        {
            action.blockTriggers = true;
        }
    }
}