using BBG.Dueling;
using BBG.Dueling.Effects;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Conditions
{
    static class ConditionCardFilter
    {
        public static void Filter(this List<Card> cards, Effect effect, IEnumerable<Condition> conditions)
        {
            cards.Filter(c => IsValid(c, effect, conditions));
        }

        public static bool IsValid(this Card card, Effect effect, IEnumerable<Condition> conditions)
        {
           return conditions.All(f => f.IsValid(new FilterArgs(card, effect)));
        }
    }
}