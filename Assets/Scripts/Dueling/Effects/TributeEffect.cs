using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/TributeEffect")]
    public class TributeEffect : CardsActionEffect
    {
        protected override DuelAction GetAction(Card target)
        {
            return new TributeAction(target, Reason.Effect, card);
        }
    }
}