using BBG.Conditions;
using BBG.Dueling.CardsGetters;
using BBG.Dueling.Selection;
using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public abstract class CardsEffect : SimpleEffect
    {
        public CardsGetter cardsGetter;
        public Condition[] cardsFilter;
        public CardsSelectionSO cardsSelection;

        protected List<Card> GetCards()
        {
            var cards = cardsGetter.GetCards(this);
            Filter(cards);
            return cards;
        }

        internal void Filter(List<Card> cards)
        {
            cards.Filter(this, cardsFilter);
            cards.Filter(c => IsResolvable(c));
        }

        protected override bool IsResolvable()
        {
            var cards = GetCards();
            if (cards.Count == 0)
                return false;
            if (cardsSelection != null && !cardsSelection.Create(this, player, cards).CanPerform())
                return false;
            return base.IsResolvable();
        }

        internal virtual bool IsResolvable(Card card) => true;

        protected override void OnResolve()
        {
            var cards = GetCards();
            var registry = duel.GetComponent<EffectRegistry>();

            if (cardsSelection == null || cards.Count == 1)
            {
                Resolve(cards);
                registry.AddSelection(card, cards);
            }
            else
            {
                var selection = cardsSelection.Create(this, player, cards);
                selection.max = ClampMaxCards(selection.max);
                selection.SetMessage();
                selection.AfterSelection += (s) =>
                {
                    Resolve(selection.Selected);
                    registry.AddSelection(card, selection.Selected);
                };
                selection.Perform();
            }
        }

        /// <summary>
        /// Features are not applied here
        /// </summary>
        internal void Resolve(Card card) => Resolve(new List<Card>(1) { card });

        protected virtual uint ClampMaxCards(uint max) => max;

        internal abstract void Resolve(List<Card> cards);
    }
}