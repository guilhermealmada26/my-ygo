using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using BBG.ValueGetters;

namespace BBG.Dueling.Effects
{
    public class CustomTributeCount : Modifier, ICustomCardValue
    {
        public IntGetter valueGetter;
        public Condition[] summonedFilter;

        public int GetCount(CardsSelection selection, Card card)
        {
            var summon = selection.CallerObject as NormalSummon;
            if (summon == null)
                return 1;
            if (!summon.card.IsValid(this, summonedFilter))
                return 1;

            var value = valueGetter.GetValue(this);
            var difference = selection.min - selection.Selected.GetCountWhere(c => c != this.card);
            if (difference <= 1)
                return 1;

            return value;
        }
    }
}