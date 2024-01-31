using BBG.ScriptableObjects;
using UnityEngine;

namespace BBG.Ygo
{
    [CreateAssetMenu(menuName = "Cards/Properties/CardType")]
    public class CardType : SpriteSO
    {
        public MainType mainType;
        public Subtype subtype;
        public int sortValue;
        public Color textColor = Color.black;
        public Color frameColor;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var other = obj as CardType;
            if (other == null)
                return false;
            return sortValue == other.sortValue;
        }

        public override int GetHashCode()
        {
            return sortValue.GetHashCode();
        }

        public bool IsContinuous => subtype is Subtype.Continuous or Subtype.Field or Subtype.Equip;

        public string DisplayName => subtype + " " + mainType;
    }
}