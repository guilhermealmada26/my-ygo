using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using UnityEngine;

namespace BBG.Dueling.PlayersGetters
{
    [CreateAssetMenu(menuName = "Effects/PlayerTargets/Trigger")]
    public class TriggerActionPlayer : PlayersGetter
    {
        public bool actionCardController;

        protected override Player[] GetPlayers(Player caller, Effect eff)
        {
            var action = eff.TriggerAction;
            if (actionCardController && action != null && action is CardAction ca)
                return new Player[] { ca.card.Controller };

            return new Player[0];
        }
    }
}