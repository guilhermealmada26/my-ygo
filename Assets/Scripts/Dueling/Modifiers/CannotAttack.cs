using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Modifiers/CannotAttack")]
    public class CannotAttack : CardActionModifier
    {
        protected override EventName Event => EventName.AttackAction;
    }
}