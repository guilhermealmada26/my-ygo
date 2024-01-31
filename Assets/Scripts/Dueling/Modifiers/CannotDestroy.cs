using BBG.Dueling.Actions;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class CannotDestroy : CardActionModifier
    {
        public Reason[] reasons;

        protected override EventName Event => EventName.DestroyAction;

        protected override bool IsValid(DuelAction action)
        {
            var destroy = action as DestroyAction;
            if (reasons.Length > 0 && !reasons.Contains(destroy.reason))
                return false;
            return base.IsValid(action);
        }
    }
}