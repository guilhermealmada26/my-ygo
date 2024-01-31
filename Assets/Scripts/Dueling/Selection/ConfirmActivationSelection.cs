using BBG.Dueling.Actions;
using BBG.Dueling.Effects;

namespace BBG.Dueling.Selection
{
    public class ConfirmActivationSelection : YesNoSelection
    {
        public Effect effect;

        public ConfirmActivationSelection(Effect effect) : base(effect.player, Message(effect))
        {
            this.effect = effect;
            AfterSelection += Activate;
        }

        static string Message(Effect effect)
        {
            var name = effect.card.data.cardName;
            return $"Do you want to activate the effect of <color=red>\"{name}\"</color>";
        }

        private void Activate(SelectionAction selection)
        {
            if (choice.Value)
            {
                var activate = new ActivationAction(effect);
                activate.Perform(Priority.AboveDefault);
            }
        }
    }
}