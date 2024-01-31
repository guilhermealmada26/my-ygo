using BBG.Dueling.Actions;
using BBG.Dueling.PlayersGetters;

namespace BBG.Dueling.Effects
{
    public abstract class PlayerActionEffect : SimpleEffect
    {
        public PlayersGetter target;

        protected override bool IsResolvable()
        {
            foreach (var p in target.GetPlayers(this))
            {
                var action = GetAction(p);
                action.CallerObject = this;
                if (!action.CanPerform())
                    return false;
            }

            return base.IsResolvable();
        }

        protected override void OnResolve()
        {
            foreach (var p in target.GetPlayers(this))
            {
                var action = GetAction(p);
                action.CallerObject = this;
                action.Perform();
                duel.GetComponent<EffectRegistry>().AddAction(card, action);
            }
        }

        protected abstract DuelAction GetAction(Player target);
    }
}