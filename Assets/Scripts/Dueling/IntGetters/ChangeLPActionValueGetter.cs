using BBG.Dueling;
using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using System.Linq;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/ChangeLPActionValueGetter")]
    public class ChangeLPActionValueGetter : DuelIntGetter
    {
        public Reason[] validReasons;

        protected override int GetValue(Card card, Effect caller)
        {
            var registry = Duel.Current.GetComponent<EffectRegistry>();
            var actions = registry.GetActions(card);
            actions.Filter((a) => a is ChangeLPAction cl && validReasons.Contains(cl.Reason));
            if (actions.Count == 0)
                return 0;
            return (actions.Last() as ChangeLPAction).amount;
        }
    }
}