using BBG.Dueling.Effects;

namespace BBG.Dueling.Actions
{
    public class ResolveAction : CardAction
    {
        public readonly Effect effect;

        public ResolveAction(Effect effect) : base(effect.card)
        {
            this.effect = effect;
            card = effect.card;
        }

        protected override void OnPerform()
        {
            effect.Resolve();
        }
    }
}