using BBG.Dueling.Actions;
using System.Collections.Generic;

namespace BBG.Dueling.Selection
{
    public class AttackSelection : CardAction
    {
        private CardsSelection selection;
        private BattleAction battle;

        public Player player;
        public List<Card> AttackTargets { internal set; get; }
        public bool optional = true;

        string Message => $"{Attacker.data.cardName} will attack:";
        public Monster Attacker => battle.attacker;
        public Monster Attacked => battle.attacked;
        public BattleAction BattleAction => battle;

        public AttackSelection(BattleAction battle) : base(battle.attacker)
        {
            player = battle.attacker.Controller;
            Caller = battle.attacker;
            this.battle = battle;
            AttackTargets = new List<Card>(Attacker.Controller.Opponent[Location.MonsterZone]);
        }

        protected override bool ValidPerform()
        {
            if (AttackTargets.Count == 0)
                return false;
            return base.ValidPerform();
        }

        protected override void BeforePerform()
        {
            selection = new CardsSelection(player, AttackTargets, 1)
            {
                message = Message,
                isOptional = optional
            };
            selection.Perform();
            selection.AfterSelection += AfterSelection;
        }

        private void AfterSelection(SelectionAction selection)
        {
            if (selection.Cancelled)
            {
                Cancel();
                battle.Cancel();
                return;
            }

            SetAttacked((selection as CardsSelection).Selected[0]);
            new DelegateAction(() =>
            {
                if (Attacked.Position == CardPosition.DefFaceDown && Attacked.Location == Location.MonsterZone)
                    new ChangePositionAction(Attacked, CardPosition.DefFaceUp) { reason = Reason.Battle }.Perform(Priority.AboveDefault);
            }).Perform(Priority.BeforeAfterChain);
        }

        public override IEnumerable<Card> GetCards()
        {
            return selection.Selected;
        }

        void SetAttacked(Card card)
        {
            battle.attacked = card as Monster;
        }
    }
}