using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/FlipPosition")]
    public class FlipPositionEffect : CardsActionEffect
    {
        protected override DuelAction GetAction(Card target)
        {
            var position = target.InAtkPosition() ? CardPosition.DefFaceUp : CardPosition.AtkFaceUp;
            return new ChangePositionAction(target, position, card);
        }
    }
}