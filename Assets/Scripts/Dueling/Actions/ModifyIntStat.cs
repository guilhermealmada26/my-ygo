namespace BBG.Dueling.Actions
{
    public class ModifyIntStat : CardAction, IRevertable
    {
        readonly int value;
        readonly CardIntField field;

        public ModifyIntStat(Monster monster, Card caller, CardIntField field, int value) : base(monster)
        {
            Caller = caller;
            reason = Reason.Effect;
            this.field = field;
            this.value = value;
        }

        protected override void OnPerform()
        {
            field.GetIntHolder(card).Add(value);
        }

        public void Revert()
        {
            field.GetIntHolder(card).Remove(value);
            Events.TriggerAction(this);
        }
    }
}