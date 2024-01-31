using BBG.Dueling.Effects;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.PlayersGetters
{
    public abstract class PlayersGetter : ScriptableObject
    {
        public Player[] GetPlayers(Effect effect)
        {
            return GetPlayers(effect.player, effect);
        }

        public Player[] GetPlayers(FilterArgs args)
        {
            return GetPlayers(args.caller.player, args.caller);
        }

        protected abstract Player[] GetPlayers(Player caller, Effect eff);

        public bool IsValid(Effect eff, Player toCheck)
        {
            return GetPlayers(eff).Contains(toCheck);
        }
    }
}