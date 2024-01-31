using BBG.Conditions;
using Sirenix.OdinInspector;

namespace BBG.Ygo
{
    [System.Serializable]
    public class MaterialData
    {
        [LabelWidth(40)]
        public Condition filter;
        [HorizontalGroup, LabelWidth(40)]
        public int min = 1;
        [HorizontalGroup, LabelWidth(40)]
        public int max = 1;
    }
}