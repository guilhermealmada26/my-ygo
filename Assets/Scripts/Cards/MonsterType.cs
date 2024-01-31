using BBG.Animations;
using BBG.ScriptableObjects;
using UnityEngine;

namespace BBG.Ygo
{
    [CreateAssetMenu(menuName = "Cards/Properties/MonsterType")]
    public class MonsterType : SpriteSO
    {
        public string typeName;
        public ParticleAnimation attackAnimation;

        public int SortValue => CardValuesHolder.MonsterTypes().IndexOf(this);
    }
}