using BBG.Dueling.Effects;

namespace BBG.Dueling.Actions
{
    public class AddEffectsAction : CardAction, IRevertable
    {
        Effect[] effects;

        public AddEffectsAction(Card target, Effect[] effects, Card caller) : base(target)
        {
            reason = Reason.Effect;
            Caller = caller;
            this.effects = effects;
        }

        protected override void OnPerform()
        {
            effects.Foreach(e => card.AddEffect(e));
        }

        public void Revert()
        {
            effects.Foreach(e => card.RemoveEffect(e));
        }
    }
}