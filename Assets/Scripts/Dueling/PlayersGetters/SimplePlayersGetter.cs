using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.PlayersGetters
{
    [CreateAssetMenu(menuName = "Effects/PlayerTargets/Simple")]
    public class SimplePlayersGetter : PlayersGetter
    {
        public bool includeSelf;
        public bool includeOpponent;

        protected override Player[] GetPlayers(Player caller, Effect eff)
        {
            var players = new List<Player>();
            if (includeSelf)
                players.Add(caller);
            if (includeOpponent)
                players.Add(caller.Opponent);
            return players.ToArray();
        }
    }
}