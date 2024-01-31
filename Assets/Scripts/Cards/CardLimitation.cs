using BBG.ScriptableObjects;
using UnityEngine;

namespace BBG.Ygo
{
    [CreateAssetMenu(menuName = "Cards/Properties/Limitation")]
    public class CardLimitation : SpriteSO
    {
        public string limitationName;
        public int maxCards;
    }
}