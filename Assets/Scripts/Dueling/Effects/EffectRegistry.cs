using BBG.Dueling.Actions;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class EffectRegistry : DuelComponent
    {
        readonly List<Activated> activated = new();
        readonly Dictionary<Card, List<Card>> lastSelectedCards = new();
        readonly Dictionary<Card, List<DuelAction>> effectActions = new();

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ActivationAction>(OnActivation);
        }

        private void OnActivation(DuelAction action)
        {
            var activation = action as ActivationAction;
            AddActivated(activation.card, activation.effect);
        }

        public void AddActivated(Card card, Effect effect)
        {
            activated.Add(new Activated { card = card, effect = effect, turnPerformed = duel.TurnCount });
        }

        public List<Activated> GetActivatedByCard(Effect effect)
        {
            return activated.Where((a) => a.effect == effect).ToList();
        }

        public List<Activated> GetActivatedByCardName(Effect effect)
        {
            return activated.Where(a => a.card.data.cardName == effect.card.data.cardName
                                     && effect.player == a.effect.player
                                     && a.effect.name == effect.name).ToList();
        }

        public void AddSelection(Card card, List<Card> selected)
        {
            lastSelectedCards[card] = selected;
        }

        public List<Card> LastSelection(Card card)
        {
            lastSelectedCards.TryGetValue(card, out List<Card> selected);
            return selected;
        }

        public void AddAction(Card card, DuelAction action)
        {
            if (!effectActions.ContainsKey(card))
                effectActions.Add(card, new List<DuelAction>(5));
            effectActions[card].Add(action);
        }

        public List<DuelAction> GetActions(Card card)
        {
            if (!effectActions.ContainsKey(card))
                return new List<DuelAction>(0);
            return new List<DuelAction>(effectActions[card]);
        }

        public class Activated
        {
            public Card card;
            public Effect effect;
            public int turnPerformed;

        }
    }
}