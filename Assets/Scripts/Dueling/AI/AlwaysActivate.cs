using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.AI
{
    [CreateAssetMenu(menuName = "Duel/AI/AlwaysActivate")]
    public class AlwaysActivate : AIDeclareAspect
    {
        public CardData[] cards;

        public override DuelAction GetAction(Player player)
        {
            var valid = player.GetActivatable(Location.Hand, Location.MonsterZone);
            foreach (var card in valid)
            {
                var action = new ActivationAction(card, Effect.Type.Activate);
                if (action.effect == null || !cards.Any(c => c.cardName == action.card.data.cardName))
                    continue;
                if(action.CanPerform())
                    return action;
            }

            return null;
        }
    }
}