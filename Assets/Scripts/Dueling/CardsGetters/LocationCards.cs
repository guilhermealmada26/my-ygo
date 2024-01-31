using BBG.Dueling.Effects;
using BBG.Dueling.PlayersGetters;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/LocationCards")]
    public class LocationCards : CardsGetter
    {
        public PlayerLocation[] locations;

        public override List<Card> GetCards(Effect caller)
        {
            var cards = new List<Card>();
            foreach (var pair in locations)
                foreach (var player in pair.target.GetPlayers(caller))
                    cards.AddRange(player[pair.location]);
            return cards;
        }

        [System.Serializable]
        public struct PlayerLocation
        {
            public PlayersGetter target;
            public Location location;
        }
    }
}