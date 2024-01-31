using BBG.Conditions;
using BBG.Dueling.Actions;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/NegateCurrentAttack")]
    public class NegateCurrentAttack : SimpleEffect
    {
        public Condition[] attackerFilter;
        public Effect afterNegate;

        protected override void OnSetup()
        {
            base.OnSetup();
            afterNegate = afterNegate.Clone(card);
        }

        internal override void BeforeRemove()
        {
            base.BeforeRemove();
            afterNegate.BeforeRemove();
        }

        protected override bool IsResolvable()
        {
            if (TriggerAction == null || TriggerAction is not AttackAction attack)
                return false;
            return attackerFilter.All(f => f.IsValid(new FilterArgs(attack.Attacker, this)));
        }

        protected override void OnResolve()
        {
            var action = TriggerAction as AttackAction;
            var card = action.Attacker;
            new NegateAction(action.Battle, card).Perform();
            if (afterNegate == null)
                return;

            afterNegate.TriggerAction = action;
            if (afterNegate is CardsEffect cardsEffect)
                cardsEffect.Resolve(card);
            else
                afterNegate.Resolve();
        }
    }
}