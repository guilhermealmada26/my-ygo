using BBG.Ygo;

namespace BBG.Dueling.Actions
{
    public class MaterialDiscard : MoveCardAction
    {
        public readonly Monster summoned;

        public MaterialDiscard(Card card, Monster summoned, Card caller)
            : base(card, summoned.Type.subtype == Subtype.Ancient ? Location.Banished : Location.Graveyard, caller)
        {
            this.summoned = summoned;
        }

        protected override void OnPerform()
        {
            base.OnPerform();
            if (destination == Location.Deck)
                card.Controller[destination].Shuffle(duel.Random);
        }
    }
}