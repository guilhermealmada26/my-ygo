using BBG.Dueling.Actions;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class CannotSummon : CardActionModifier
    {
        public EventName summonType;
        public Reason[] reasons;

        protected override EventName Event => summonType;

        protected override bool IsValid(DuelAction action)
        {
            var destroy = action as DestroyAction;
            if (reasons.Length > 0 && !reasons.Contains(destroy.reason))
                return false;
            return base.IsValid(action);
        }
    }
}