using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public abstract class ExtraSummonEffect : SimpleEffect
    {
        protected abstract List<Card> GetSummonables();
        protected abstract ExtraSummon GetSummonAction(Card toSummon);

        protected override bool IsResolvable() => GetSummonables().Count > 0;

        protected override void OnResolve()
        {
            var selection = new CardsSelection(player, GetSummonables(), 1);
            selection.message = "Select the monster to be summoned.";
            selection.Perform();
            selection.AfterSelection += (s) =>
            {
                Card summoned = selection.Selected[0];
                var summon = GetSummonAction(summoned);
                summon.Perform();
            };
        }
    }
}