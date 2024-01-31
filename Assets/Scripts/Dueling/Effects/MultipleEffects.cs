using BBG.Dueling.Actions;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/MultipleEffects")]
    public class MultipleEffects : SimpleEffect
    {
        public bool delayResolve;
        public Effect[] effects = new Effect[2];

        internal override DuelAction TriggerAction
        {
            get => base.TriggerAction;
            set
            {
                base.TriggerAction = value;
                effects.Foreach(e => e.TriggerAction = value);
            }
        }

        protected override void OnSetup()
        {
            effects = effects.Clone(card);
        }

        protected override bool IsResolvable()
        {
            if(!effects.All(e => e.CanResolve()))
                return false;
            return base.IsResolvable();
        }

        public override void OnActivate()
        {
            base.OnActivate();
            effects.Foreach(e => e.OnActivate());
        }

        protected override void OnResolve()
        {
            foreach (var eff in effects)
            {
                if (delayResolve)
                    new DelegateAction(eff.Resolve).Perform(Priority.DelayedAction);
                else
                    eff.Resolve();
            }
        }

        protected override void OnRevert()
        {
            effects.Foreach(e => e.Revert());
        }

        internal override void BeforeRemove()
        {
            base.BeforeRemove();
            effects.Foreach(e => e.BeforeRemove());
        }
    }
}