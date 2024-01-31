using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/ChangeControl")]
    public class ChangeControlEffect : CardsActionEffect
    {
        public Location location;

        protected override DuelAction GetAction(Card target)
        {
            return new ChangeControlAction(target, location, card);
        }
    }
}