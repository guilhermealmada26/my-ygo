using System.Collections.Generic;

namespace BBG.Ygo
{
    [System.Serializable]
    public class CardComparer : IComparer<CardData>
    {
        public enum Criteria
        {
            CardType, Name, Attribute, MType, Atk, Def, Lvl
        }
        public Criteria sortCriteria;
        public bool reverseOrder;

        public CardComparer()
        {
            sortCriteria = Criteria.CardType;
        }

        public void Sort(List<CardData> cards)
        {
            if (cards == null)
                return;
            cards.Sort(this);
            if (reverseOrder)
                cards.Reverse();
        }

        public int Compare(CardData x, CardData y)
        {
            var v1 = GetValue(x);
            var v2 = GetValue(y);
            if (v1 != v2)
                return v1.CompareTo(v2);
            return x.cardName.CompareTo(y.cardName);
        }

        const int INVALID_HIGH = 8888;
        const int INVALID_LOW = -8888;

        private int GetValue(CardData data)
        {
            return sortCriteria switch
            {
                Criteria.CardType => data.type.sortValue,
                Criteria.Atk => (data is MonsterData m) ? m.atk : INVALID_LOW,
                Criteria.Def => (data is MonsterData m) ? m.def : INVALID_LOW,
                Criteria.Lvl => (data is MonsterData m) ? m.lvl : INVALID_LOW,
                Criteria.Attribute => (data is MonsterData m) ? m.attribute.SortValue : INVALID_HIGH,
                Criteria.MType => (data is MonsterData m) ? m.monsterType.SortValue : INVALID_HIGH,
                _ => INVALID_HIGH,
            };
        }
    }
}