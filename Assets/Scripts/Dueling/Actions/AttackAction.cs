using BBG.Dueling.Selection;

namespace BBG.Dueling.Actions
{
    public class AttackAction : CardAction
    {
        public bool IsDirect { internal set; get; }
        public bool ValidPosition { internal set; get; }
        internal int MaxAttacks { set; get; } = 1;
        internal BattleAction Battle { private set; get; }

        public Monster Attacker => card as Monster;
        private Player player => Attacker.Controller;

        public AttackAction(Monster attacker) : base(attacker)
        {
            var oppMonsterCount = player.Opponent[Location.MonsterZone].Count;
            IsDirect = oppMonsterCount == 0;
            Battle = new BattleAction(Attacker, null);
            ValidPosition = attacker.InAtkPosition();
        }

        protected override bool ValidPerform()
        {
            if (duel.Phase != DuelPhase.Battle)
                return false;
            if (Attacker.Location != Location.MonsterZone)
                return false;
            //opp can have non attackable monsters in field by this specific attacker 
            var oppMonsterCount = player.Opponent[Location.MonsterZone].Count;
            if (oppMonsterCount > 0 && !new AttackSelection(Battle).CanPerform())
                return false;
            var attacksThisTurn = TurnActionsRegistry.Instance.PerformedThisTurn<BattleAction>();
            attacksThisTurn.Filter(a => a.attacker == Attacker);
            if (!ValidPosition || attacksThisTurn.Count >= MaxAttacks)
                return false;
            return base.ValidPerform();
        }

        protected override void BeforePerform()
        {
            new DelegateAction(TrySelectTarget).Perform();
        }

        private void TrySelectTarget()
        {
            if (!IsDirect)
                new AttackSelection(Battle).Perform();
        }

        protected override void OnPerform()
        {
            if (Cancelled || !IsDirect && Battle.attacked == null)
            {
                Cancel();
                return;
            }

            new DelegateAction(PerformBattle).Perform(Priority.AfterChain);
        }

        private void PerformBattle()
        {
            //gravity bind cancels attack here
            Events.TriggerValidate(this);
            if (!Cancelled)
            {
                TurnActionsRegistry.Instance.Add(Battle);
                Battle.Perform();
            }
        }
    }
}