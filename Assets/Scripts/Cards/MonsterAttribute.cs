using BBG.Animations;
using BBG.ScriptableObjects;
using UnityEngine;

namespace BBG.Ygo
{
    [CreateAssetMenu(menuName = "Cards/Properties/Attribute")]
    public class MonsterAttribute : SpriteSO
    {
        public string attributeName;
        public Color color;
        public ParticleAnimation attackAnimation;

        public int SortValue => CardValuesHolder.Attributes().IndexOf(this);
    }
}