using BBG.Dueling;
using BBG.Dueling.Effects;
using BBG.Dueling.PlayersGetters;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/LifePointsGetter")]
    public class LifePointsGetter : DuelIntGetter
    {
        public float multiplier = 1f;
        public PlayersGetter target;

        protected override int GetValue(Card card, Effect caller)
        {
            var player = target.GetPlayers(caller)[0];
            return (int)(player.LP * multiplier);
        }
    }
}