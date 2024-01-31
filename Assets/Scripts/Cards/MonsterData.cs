using BBG.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BBG.Ygo
{
    public class MonsterData : CardData
    {
        [Title("Monster Properties")]
        [GUIColor(nameof(AttributeColor))]
        public MonsterAttribute attribute;
        public MonsterType monsterType;
        [HorizontalGroup("ad"), LabelWidth(45)]
        public int atk;
        [HorizontalGroup("ad"), LabelWidth(45)]
        public int def;
        [Range(1, 12)]
        public int lvl;
        [PreviewField(66), Title("")]
        public Sprite hologram;
        [SerializeField]
        ParticleAnimation atkAnimation;

        public override bool IsMonster => true;
        public override bool GoesToExtraDeck => type.subtype == Subtype.Ritual;
        internal bool? ShowNumericValuesOnly { set; get; }

        public ParticleAnimation AttackAnimation
        {
            get
            {
                if (atkAnimation != null)
                    return atkAnimation;
                if (monsterType.attackAnimation != null)
                    return monsterType.attackAnimation;
                return attribute.attackAnimation;
            }
        }

        public string AtkText()
        {
            if (ShowNumericValuesOnly.HasValue && ShowNumericValuesOnly.Value)
                return atk.ToString();
            var custom = CardDatabase.GetCustomAtk(this);
            return string.IsNullOrEmpty(custom) ? atk.ToString() : custom;
        }

        public string DefText()
        {
            if (ShowNumericValuesOnly.HasValue && ShowNumericValuesOnly.Value)
                return def.ToString();
            var custom = CardDatabase.GetCustomDef(this);
            return string.IsNullOrEmpty(custom) ? def.ToString() : custom;
        }

        private Color AttributeColor() => attribute.color;
    }
}