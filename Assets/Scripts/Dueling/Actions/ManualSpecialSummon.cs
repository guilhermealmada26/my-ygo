using BBG.Dueling.Effects;
using System.Linq;

namespace BBG.Dueling.Actions
{
    public class ManualSpecialSummon : CardAction
    {
        private Effect effect;

        public ManualSpecialSummon(Monster monster) : base(monster)
        {
            effect = monster.effects.FirstOrDefault(e => e.type == Effect.Type.SpecialSummon);
        }

        protected override bool ValidPerform()
        {
            if (card.Location.IsField())
                return false;
            if(effect == null || !effect.CanResolve())
                return false;
            return base.ValidPerform();
        }

        protected override void OnPerform()
        {
            effect.OnActivate();
            new DelegateAction(effect.Resolve).Perform();
        }
    }
}