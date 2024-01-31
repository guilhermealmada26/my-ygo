using BBG.Dueling.Actions;
using BBG.ValueGetters;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/PlayerTarget/ChangeLP")]
    public class ChangeLPEffect : PlayerActionEffect
    {
        public IntGetter value;
        public bool heal;
        public Reason reason = Reason.Effect;

        protected override DuelAction GetAction(Player target)
        {
            var v = value.GetValue(this);
            return new ChangeLPAction(target, heal ? -v : v, reason, card);
        }
    }
}