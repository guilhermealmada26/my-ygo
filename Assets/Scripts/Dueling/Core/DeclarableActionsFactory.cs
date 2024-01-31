using BBG.Dueling.Actions;
using System.Collections.Generic;

namespace BBG.Dueling
{
    public class DeclarableActionsFactory
    {
        public List<DuelAction> Get(Card card)
        {
            var actions = new List<DuelAction>(3);
            if (card.Location == Location.Hand || card.Location.IsField())
                actions.Add(new ActivationAction(card, Effects.Effect.Type.Activate));
            else
                actions.Add(new ActivationAction(card, Effects.Effect.Type.ZoneActivate));

            if (card is Monster monster)
                AddMonsterActions(monster, actions);
            else
                AddSpellTrapActions(card, actions);

            actions.Filter(a => a.CanPerform());
            return actions;
        }

        private void AddSpellTrapActions(Card card, List<DuelAction> actions)
        {
            if(card.Location == Location.Hand && Duel.Current.Phase == DuelPhase.Main)
                actions.Add(new SetCardAction(card));
        }

        private void AddMonsterActions(Monster card, List<DuelAction> actions)
        {
            //effect special summon (not respondable and do not show activate )
            if (card.Location != Location.MonsterZone)
                actions.Add(new ManualSpecialSummon(card));

            var duel = Duel.Current;
            if (card.Location == Location.Hand && duel.Phase == DuelPhase.Main)
            {
                actions.Add(new NormalSummon(card));
                actions.Add(new SetSummon(card));   
            }
            else if(card.Location == Location.MonsterZone)
            {
                if (duel.Phase == DuelPhase.Main)
                {
                    actions.Add(card.IsFaceUp() ? new FlipPositionAction(card) : new FlipSummon(card));
                }
                else if (duel.Phase == DuelPhase.Battle)
                {
                    actions.Add(new AttackAction(card));
                }
            }
            else if(card.Location == Location.ExtraDeck && card.data.type.subtype == Ygo.Subtype.Ancient && duel.Phase == DuelPhase.Main)
            {
                actions.Add(new AncientFusionSummon(card));
            }
        }
    }
}