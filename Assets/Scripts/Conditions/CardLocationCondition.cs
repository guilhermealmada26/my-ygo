using BBG.Dueling;
using BBG.Dueling.Effects;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Cards/CardLocationCondition")]
    public class CardLocationCondition : CardCondition
    {
        public bool previousLocation;
        public Location[] locations;

        protected override bool IsValid(Card card, Player player, Effect effect)
        {
            var location = previousLocation ? card.location.Previous : card.Location;
            return locations.Contains(location);
        }
    }
}