using BBG.Dueling.Effects;
using BBG.ScriptableObjects;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo
{
    [HideMonoScript]
    public class CardData : ScriptableObject
    {
        [PreviewField(120, Alignment = ObjectFieldAlignment.Center), HideLabel]
        public Sprite sprite;
        public string cardName;
        [GUIColor(nameof(TypeColor))]
        public CardType type;
        public CardLimitation limitation;
        [MultiLineProperty(4), HideLabel]
        public string description;
        public List<StringSO> archetypes = new();
        public Effect[] effects;

        private CardData data;

        public string ID => name;
        public virtual bool IsMonster => false;
        public virtual bool GoesToExtraDeck => false;

        public CardData OriginalData
        {
            get => data == null ? this : data;
            set => data = value;
        }

        public CardData Clone()
        {
            return Instantiate(this);
        }

        private Color TypeColor() => type.frameColor;
    }
}