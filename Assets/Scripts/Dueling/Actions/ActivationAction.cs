using BBG.Dueling.Effects;
using BBG.Ygo;

namespace BBG.Dueling.Actions
{
    public class ActivationAction : CardAction
    {
        public Effect effect { private set; get; }
        public int ChainLink { private set; get; }

        public Player player => card.Controller;

        public ActivationAction(Card card, int effectIndex) : base(card)
        {
            effect = card.effects[effectIndex];
        }

        public ActivationAction(Effect effect) : base(effect.card)
        {
            this.effect = effect;
        }

        public ActivationAction(Card card, Effect.Type type) : base(card)
        {
            foreach (var effect in card.effects)
                if (effect != null && effect.type == type && effect.CanResolve())
                    this.effect = effect;
        }

        protected override bool ValidPerform()
        {
            if (effect == null)
                return false;
            if (card.Type.mainType == MainType.Spell && card.Location == Location.Hand &&
                player[Location.STZone].Count >= player.FieldMaxCards)
                return false;
            if (card.Type.mainType == MainType.Trap)
            {
                if (card.Location == Location.Hand)
                    return false;
                var setted = TurnActionsRegistry.Instance.PerformedThisTurn<SetCardAction>(card);
                if (card.Location == Location.STZone && setted.Count > 0)
                    return false;
            }
            if (!effect.CanResolve())
                return false;
            if (priority > Priority.AfterChain && !duel.GetComponent<ChainSystem>().CanAddLink(effect))
                return false;
            if (effect.type is Effect.Type.Activate or Effect.Type.ZoneActivate && effect.SpeedSpell() <= 1 && duel.Phase != DuelPhase.Main)
                return false;
            return base.ValidPerform();
        }

        protected override void BeforePerform()
        {
            if (!card.IsFaceUp())
                new ChangePositionAction(card).Perform();
            if (card.Type.mainType == MainType.Spell && card.Location == Location.Hand)
                new MoveCardAction(card, Location.STZone).Perform();
        }

        protected override void OnPerform()
        {
            var chain = duel.GetComponent<ChainSystem>();
            effect.OnActivate();
            ChainLink = chain.AddLink(effect);
            var type = card.Type;
            if (type.mainType != MainType.Monster && !type.IsContinuous && card.Location.IsField())
                new DiscardResolved(card).Perform(Priority.SendResolvedCardsToGY);
        }
    }
}