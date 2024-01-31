namespace BBG.Dueling.Actions
{
    public abstract class SummonAction : CardAction
    {
        public Player player;
        public CardPosition position;
        public int cardsToRemoveBeforeSummon; //effects can set it too

        public Monster Monster => card as Monster;

        public SummonAction(Player player, Monster monster) : base(monster)
        {
            this.player = player;
        }

        protected override bool ValidPerform()
        {
            var count = player[Location.MonsterZone].Count - cardsToRemoveBeforeSummon;
            if (Monster.Location != Location.MonsterZone && count >= player.FieldMaxCards)
                return false;
            return base.ValidPerform();
        }

        internal bool CanSummon() => ValidPerform();

        protected override void OnPerform()
        {
            Monster.LastSummon = this;
            Monster.SetLocation(Location.MonsterZone, player);
            Monster.position.Current = position;
            Events.TriggerAction(this, EventName.SummonAction.ToString());
        }
    }
}