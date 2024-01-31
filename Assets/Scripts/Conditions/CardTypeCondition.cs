using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/CardData/CardType")]
    public class CardTypeCondition : CardDataCondition
    {
        public CardType[] types = new CardType[0];
        public MainType[] mainTypes = new MainType[0];
        public Subtype[] subtypes = new Subtype[0];

        protected override bool IsValid(CardData data)
        {
            if (mainTypes.Length > 0 && !mainTypes.Contains(data.type.mainType))
                return false;
            if (types.Length > 0 && !types.Contains(data.type))
                return false;
            if (subtypes.Length > 0 && !subtypes.Contains(data.type.subtype))
                return false;
            return true;
        }
    }
}