using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    public class CardValuesHolderFilter : CardsFilter
    {
        readonly CardTypeCondition cardType;
        readonly AttributeCondition attribute;
        readonly CardLimitationCondition limitation;
        readonly MonsterTypeCondition monsterType;
        readonly MonsterLevelCondition levels;
        readonly CardNameCondition cardName;

        public CardValuesHolderFilter()
        {
            cardType = ScriptableObject.CreateInstance<CardTypeCondition>();
            attribute = ScriptableObject.CreateInstance<AttributeCondition>();
            limitation = ScriptableObject.CreateInstance<CardLimitationCondition>();
            monsterType = ScriptableObject.CreateInstance<MonsterTypeCondition>();
            levels = ScriptableObject.CreateInstance<MonsterLevelCondition>();
            cardName = ScriptableObject.CreateInstance<CardNameCondition>();
            Reset();
        }

        public void Reset()
        {
            cardType.types = new CardType[0];
            attribute.attributes = new MonsterAttribute[0];
            limitation.limitations = new CardLimitation[0];
            monsterType.types = new MonsterType[0];
            levels.levels = new int[0];
            cardName.substring = string.Empty;
        }

        public override bool IsValid(CardData card)
        {
            return attribute.IsValid(card) && limitation.IsValid(card)
                && monsterType.IsValid(card) && levels.IsValid(card)
                && cardName.IsValid(card) && cardType.IsValid(card);
        }

        public void SetCardTypes(string[] names)
        {
            cardType.types = new CardType[names.Length];
            for (int i = 0; i < names.Length; i++)
                cardType.types[i] = CardValuesHolder.Types().First(t => t.DisplayName == names[i]);
        }

        public void SetAttributes(string[] names)
        {
            attribute.attributes = new MonsterAttribute[names.Length];
            for (int i = 0; i < names.Length; i++)
                attribute.attributes[i] = CardValuesHolder.Attributes().First(t => t.attributeName == names[i]);
        }

        public void SetLimitations(string[] names)
        {
            limitation.limitations = new CardLimitation[names.Length];
            for (int i = 0; i < names.Length; i++)
                limitation.limitations[i] = CardValuesHolder.Limitations().First(t => t.limitationName == names[i]);
        }

        public void SetMonsterType(string[] names)
        {
            monsterType.types = new MonsterType[names.Length];
            for (int i = 0; i < names.Length; i++)
                monsterType.types[i] = CardValuesHolder.MonsterTypes().First(t => t.typeName == names[i]);
        }

        public void SetLevels(int[] indexes)
        {
            for (int i = 0; i < indexes.Length; i++)
                indexes[i] += 1;
            levels.levels = indexes;
        }

        public void SetNameSubstring(string substring)
        {
            cardName.substring = substring;
        }
    }
}