namespace BBG.Dueling.Selection
{
    public class YesNoSelection : SelectionAction
    {
        public bool? choice;

        public YesNoSelection(Player player, string message) : base(player)
        {
            this.message= message;
        }

        public override bool RequirementsAreDone()
        {
            return choice.HasValue;
        }

        public override string GetLoadableData()
        {
            return choice.ToString();
        }

        protected override void SetLoadableData(string data)
        {
            choice = bool.Parse(data);
        }
    }
}