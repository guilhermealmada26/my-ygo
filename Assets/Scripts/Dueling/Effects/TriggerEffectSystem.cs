using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class TriggerEffectSystem : DuelComponent
    {
        Dictionary<EventName, List<Effect>> effects = new();

        public void Add(EventName name, Effect effect)
        {
            if (!effects.ContainsKey(name))
            {
                effects.Add(name, new List<Effect>());
                Events.Observe(name, ActionPerformed);
            }
            effects[name].Add(effect);
        }

        public void Remove(EventName name, Effect effect)
        {
            if (effects.ContainsKey(name))
                effects[name].Remove(effect);
        }

        void ActionPerformed(DuelAction action)
        {
            if (action.blockTriggers)
                return;
            var name = Enum.Parse<EventName>(action.GetType().Name);
            if (!effects.ContainsKey(name))
                return;

            var player = FirstPlayer(action);
            var playerResponds = new List<Effect>(5);
            var oppResponds = new List<Effect>(5);
            var priority = duel.GetComponent<ChainSystem>().IsResolving ? Priority.Triggered : Priority.Default;
            var toReset = new List<Effect>(5);

            foreach (var eff in effects[name])
            {
                var responds = eff.player == player ? playerResponds : oppResponds;
                if (eff.TriggerAction != null)
                    continue;

                eff.TriggerAction = action;
                if (!eff.CanResolve())
                {
                    eff.TriggerAction = null;
                    continue;
                }

                toReset.Add(eff);
                if (eff.type == Effect.Type.Trigger)
                {
                    if (eff.features.Any(f => f is ResolveImmediately))
                    {
                        eff.Resolve();
                        eff.TriggerAction = null;
                    }
                    else if (eff.card.IsFaceUp())
                    {
                        void activate() { new ActivationAction(eff).Perform(Priority.AboveDefault); }
                        new DelegateAction(activate).Perform(priority);
                    }
                    else
                    {
                        //face down effects are given the opportunity to ask for activation
                        responds.Add(eff);
                    }
                }
                else if (eff.type == Effect.Type.OptionalTrigger)
                    new ConfirmActivationSelection(eff).Perform(priority);
                else
                    responds.Add(eff);
            }

            new CheckResponds(playerResponds, player).Perform(priority);
            new CheckResponds(oppResponds, player.Opponent).Perform(priority);
            new DelegateAction(() => ResetEffects(toReset)).Perform(Priority.AfterTriggers);
        }

        Player FirstPlayer(DuelAction action)
        {
            if (action is SummonAction summon)
                return summon.player;
            if (action is PlayerTargetAction p)
                return p.player.Opponent;
            if (action is CardAction c)
                return c.card.Controller.Opponent;
            return duel.CurrentPlayer.Opponent;
        }

        void ResetEffects(List<Effect> effects)
        {
            effects.ForEach(e => e.TriggerAction = null);
        }
    }
}