using BBG.Dueling;
using BBG.Dueling.Effects;
using BBG.Dueling.PlayersGetters;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/DuelPhaseCondition")]
    public class DuelPhaseCondition : Condition
    {
        public DuelPhase[] valid;
        public PlayersGetter target;

        public override bool IsValid(object args)
        {
            if (args is Effect effect && !target.IsValid(effect, Duel.Current.CurrentPlayer))
                return false;
            return valid.Contains(Duel.Current.Phase);
        }
    }
}