using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/CustomSpeedSpell")]
    public class CustomSpeedSpell : Feature
    {
        [Range(1, 5)]
        public int value;
    }
}