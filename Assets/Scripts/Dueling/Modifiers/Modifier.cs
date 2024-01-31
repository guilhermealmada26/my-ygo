using System.Linq;

namespace BBG.Dueling.Effects
{
    public abstract class Modifier : Effect
    {
        public bool applyOnSetup;
        public bool isFaceUpEffect;

        protected virtual bool IsValid()
        {
            if (isFaceUpEffect && !card.Location.IsField() || !card.IsFaceUp())
                return false;
            if (!conditions.All(c => c.IsValid(this)))
                return false;
            return true;
        }

        protected bool CardIsUnnafected(Card target)
        {
            var first = target.effects.FirstOrDefault(e => e is UnnaffectedByEffects) as UnnaffectedByEffects;
            if (first == null)
                return false;
            return first.Applies(this.card);
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            if (applyOnSetup)
                Resolve();
        }

        internal override void BeforeRemove()
        {
            base.BeforeRemove();
            if (applyOnSetup)
                Revert();
        }
    }
}