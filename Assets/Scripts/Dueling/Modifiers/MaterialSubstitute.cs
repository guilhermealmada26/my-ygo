using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Ygo;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Modifiers/MaterialSubstitute")]
    public class MaterialSubstitute : Modifier
    {
        public Condition[] summonedFilter;
        public enum ConditionType { Named, Attribute, MType }
        public ConditionType conditionType;

        public bool Applies(AncientFusionSummon summon, MaterialData material)
        {
            if (summon.Data.doNotAllowSubstitutes)
                return false;
            if (!summon.summoned.IsValid(this, summonedFilter))
                return false;
            if (conditionType == ConditionType.Named && material.filter is not CardNameCondition)
                return false;
            if (conditionType == ConditionType.Attribute && material.filter is not AttributeCondition)
                return false;
            if (conditionType == ConditionType.MType && material.filter is not MonsterTypeCondition)
                return false;
            return true;
        }
    }
}