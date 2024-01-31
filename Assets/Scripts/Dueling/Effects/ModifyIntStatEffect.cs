using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.ValueGetters;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class ModifyIntStatEffect : ModifyCardsEffect
    {
        [System.Serializable]
        public class Mod
        {
            public Condition condition;
            public CardIntField field;
            public IntGetter valueGetter;
        }
        public Mod[] modifiers;

        internal override bool IsResolvable(Card card)
        {
            return card is Monster && modifiers.Any(m => m.condition.IsValid(new FilterArgs(card, this)));
        }

        protected override void AddActions(List<DuelAction> actions, Card target)
        {
            var args = new FilterArgs(target as Monster, this);
            foreach (var mod in modifiers)
            {
                if (!mod.condition.IsValid(args))
                    continue;
                var action = new ModifyIntStat(target as Monster, card, mod.field, mod.valueGetter.GetValue(args));
                actions.Add(action);
            }
        }
    }
}