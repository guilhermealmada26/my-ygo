using BBG.Dueling.Selection;

namespace BBG.Dueling.Actions
{
    public class SpecialSummon : SummonAction
    {
        public bool selectPosition;

        public SpecialSummon(Player player, Monster monster, Card caller) : base(player, monster)
        {
            selectPosition = true;
            Caller = caller;
            reason = Reason.Effect;
        }

        public SpecialSummon(Player player, Monster monster, CardPosition position, Card caller) : base(player, monster)
        {
            this.position = position;
            Caller = caller;
            reason = Reason.Effect;
        }

        protected override bool ValidPerform()
        {
            if (card == null)
                return false;
            if(reason == Reason.Effect && card.Location != Location.ExtraDeck &&
                (card.data.GoesToExtraDeck || card.Type.subtype == Ygo.Subtype.Ritual))
            {
                if (Monster.LastExtraSummon == null)
                    return false;
            }
            return base.ValidPerform();
        }

        protected override void BeforePerform()
        {
            if (selectPosition)
            {
                PositionSelection selection = new(player, Monster);
                selection.AfterSelection += (s) => position = selection.position.Value;
                selection.Perform();
            }
        }
    }
}