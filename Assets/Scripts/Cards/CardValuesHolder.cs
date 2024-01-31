using UnityEngine;

namespace BBG.Ygo
{
    public static class CardValuesHolder
    {
        static CardType[] types;
        static MonsterAttribute[] attributes;
        static MonsterType[] monsterTypes;
        static CardLimitation[] limitations;

        public static CardType[] Types()
        {
            if (types == null)
                types = Resources.LoadAll<CardType>("CardTypes/");
            return types;
        }

        public static MonsterAttribute[] Attributes()
        {
            if (attributes == null)
                attributes = Resources.LoadAll<MonsterAttribute>("MonsterAttributes/");
            return attributes;
        }

        public static MonsterType[] MonsterTypes()
        {
            if (monsterTypes == null)
                monsterTypes = Resources.LoadAll<MonsterType>("MonsterTypes/");
            return monsterTypes;
        }

        public static CardLimitation[] Limitations()
        {
            if (limitations == null)
                limitations = Resources.LoadAll<CardLimitation>("Limitations/");
            return limitations;
        }
    }
}