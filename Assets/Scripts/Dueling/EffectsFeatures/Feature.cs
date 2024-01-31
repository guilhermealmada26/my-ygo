using UnityEngine;

namespace BBG.Dueling.Effects
{
    public abstract class Feature : ScriptableObject
    {
        protected Effect effect { private set; get; }

        protected Card card => effect.card;

        internal void Setup(Effect effect)
        {
            this.effect = effect;
            OnSetup();
        }

        protected virtual void OnSetup() { }

        internal virtual void BeforeRemove() { }

        internal virtual void OnResolve() { }

        internal virtual void OnRevert() { }
    }
}