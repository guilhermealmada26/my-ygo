using BBG.Dueling.Selection;
using BBG.OptionsSelection;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    public class ChoiceEffects : SimpleEffect
    {
        [TextArea(2, 3)]
        public string[] descriptions = new string[2];
        public Effect[] effects = new Effect[2];

        protected override void OnSetup()
        {
            effects = effects.Clone(card);
        }

        protected override bool IsResolvable()
        {
            return effects.Any(e => e.CanResolve());
        }

        protected override void OnResolve()
        {
            var options = new Option[effects.Length];
            for (int i = 0; i < effects.Length; i++)
            {
                options[i] = new Option
                {
                    text = descriptions[i],
                    isDisabled = !effects[i].CanResolve()
                };
            }

            var message = "Select an effect:";
            var selection = new OptionSelection(player, message, options);
            selection.Perform();
            selection.AfterSelection += (s) =>
            {
                effects[selection.choice.Value].Resolve();
            };
        }
    }
}