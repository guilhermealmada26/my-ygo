using BBG.Dueling.Effects;
using BBG.Dueling.Selection;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Actions
{
    public class CheckResponds : DuelAction
    {
        Player player;
        Effect[] responds;

        public CheckResponds(IEnumerable<Effect> responds, Player player)
        {
            this.player = player;
            this.responds = responds.Where(e => ValidEffect(e)).ToArray();
        }

        private bool ValidEffect(Effect effect)
        {
            if (player != effect.player)
                return false;
            return new ActivationAction(effect).CanPerform();
        }

        protected override bool ValidPerform() => responds.Length > 0;

        protected override void OnPerform()
        {
            responds = responds.Where(e => ValidEffect(e)).ToArray();
            var cards = new List<Card>();
            foreach (var args in responds)
                cards.Add(args.card);
            var selection = new CardsSelection(player, cards, 1)
            {
                isOptional = true,
                message = "Do you want to activate an effect?",
            };
            selection.Perform(Priority.BelowDefault);
            selection.AfterSelection += (s) =>
            {
                if (s.Cancelled)
                    return;
                var eff = responds.FirstOrDefault(r => r.card == selection.Selected[0]);
                if (eff != null)
                {
                    var action = new ActivationAction(eff);
                    action.Perform();
                }
            };
        }
    }
}