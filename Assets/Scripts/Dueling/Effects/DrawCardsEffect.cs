using BBG.Dueling.Actions;
using BBG.ValueGetters;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/PlayerTarget/DrawCards")]
    public class DrawCardsEffect : PlayerActionEffect
    {
        public IntGetter value;

        protected override DuelAction GetAction(Player target)
        {
            return new DrawCardsAction(target, value.GetValue(this), Reason.Effect);
        }
    }
}