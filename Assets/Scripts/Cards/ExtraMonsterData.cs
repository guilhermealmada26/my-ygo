using Sirenix.OdinInspector;

namespace BBG.Ygo
{
    public class ExtraMonsterData : MonsterData
    {
        [Title("Materials Properties"), LabelWidth(180)]
        public bool doNotAllowSubstitutes;
        public MaterialData[] materials;

        public override bool GoesToExtraDeck => true;
        public string SelectionMessage => "Select " + description.Split('.')[0];
    }
}