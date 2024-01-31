using BBG.OptionsSelection;

namespace BBG.Dueling.Selection
{
    public class OptionSelection : SelectionAction
    {
        public Option[] Options { private set; get; }
        public int? choice;

        public OptionSelection(Player player, string message, params Option[] options) : base(player)
        {
            Options = options;
            this.message = message;
        }

        public override bool RequirementsAreDone()
        {
            return choice.HasValue && choice.Value >= 0 && choice.Value < Options.Length;
        }

        public override string GetLoadableData()
        {
            return choice.ToString();
        }

        protected override void SetLoadableData(string data)
        {
            choice = int.Parse(data);
        }
    }
}