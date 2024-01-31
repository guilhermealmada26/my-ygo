using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/ContinuousEffectFeature")]
    public class ContinuousEffectFeature : OnLocationFeature
    {
        public bool applyOnly;
        public bool revertOnly;

        protected override void OnApply()
        {
            card.position.ValueChanged += OnPositionChanged;
            TryResolve();
        }

        protected override void OnUnApply()
        {
            card.position.ValueChanged -= OnPositionChanged;
            TryRevert();
        }

        private void OnPositionChanged()
        {
            if (card.IsFaceUp())
                TryResolve();
            else
                TryRevert();
        }

        void TryResolve()
        {
            if (revertOnly)
                return;
            effect.Resolve();
        }

        void TryRevert()
        {
            if (applyOnly)
                return;
            effect.Revert();
        }
    }
}