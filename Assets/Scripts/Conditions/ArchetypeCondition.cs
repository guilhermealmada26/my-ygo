using BBG.ScriptableObjects;
using BBG.Ygo;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/CardData/Archetype")]
    public class ArchetypeCondition : CardDataCondition
    {
        public StringSO archetype;

        protected override bool IsValid(CardData data)
        {
            return data.archetypes.Contains(archetype);
        }
    }
}