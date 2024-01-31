using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public class AddEffects : CardsActionEffect
    {
        public Effect[] effects;

        protected override DuelAction GetAction(Card target)
        {
            return new AddEffectsAction(target, effects, card);
        }
    }
}