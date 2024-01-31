using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Modifiers/CannotActivate")]
    public class CannotActivate : CardActionModifier
    {
        protected override EventName Event => EventName.ActivationAction;
    }
}