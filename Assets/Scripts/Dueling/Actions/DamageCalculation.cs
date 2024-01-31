using System.Collections.Generic;

namespace BBG.Dueling.Actions
{
    public class DamageCalculation : CardAction, ICardsHolder
    {
        private BattleAction battle;

        public DamageCalculation(BattleAction battle) : base(battle.card)
        {
            this.battle = battle;
            Caller = battle.Caller;
        }

        public override IEnumerable<Card> GetCards()
        {
            return battle.GetCards();
        }
    }
}