using BBG.Dueling;
using BBG.Dueling.Effects;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/MaxActivationCondition")]
    public class MaxActivationCondition : Condition
    {
        public int maxCount = 1;
        public bool thisTurn;
        public bool byCardName;

        public override bool IsValid(object args)
        {
            if (args is not Effect effect)
                return false;

            var duel = Duel.Current;
            var registry = duel.GetComponent<EffectRegistry>();
            var activated = byCardName ? registry.GetActivatedByCardName(effect) : registry.GetActivatedByCard(effect);
            if (thisTurn)
            {
                activated.Filter(a => a.turnPerformed == duel.TurnCount);
            }
            return activated.Count < maxCount;
        }
    }
}