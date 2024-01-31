using BBG.Dueling.Effects;
using BBG.ValueGetters;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Selection
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CardsSelectionSO")]
    public class CardsSelectionSO : ScriptableObject
    {
        public IntGetter min, max;
        public bool opponentSelects, setAllFaceUp;

        public CardsSelection Create(Effect effect, Player player, List<Card> range)
        {
            var selectioner = opponentSelects ? player.Opponent : player;
            var umin = (uint) min.GetValue(effect);
            var umax = (uint) max.GetValue(effect);

            return new CardsSelection(selectioner, range, umin, umax)
            {
                setAllFaceUp = setAllFaceUp,
            };
        }
    }
}