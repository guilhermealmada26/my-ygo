using BBG.Dueling.PlayersGetters;

namespace BBG.Dueling.Effects
{
    public abstract class PlayerActionModifier : ActionModifier
    {
        public PlayersGetter target;

        protected bool IsValid(Player toCheck)
        {
            if (!IsValid())
                return false;
            //if (target == PlayerTarget.Both)
            //    return true;
            //if (target == PlayerTarget.Opponent)
            //    return toCheck == player.Opponent;
            return target.IsValid(this, toCheck);
        }
    }
}