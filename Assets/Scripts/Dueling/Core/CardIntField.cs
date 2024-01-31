using BBG.Ygo;

namespace BBG.Dueling
{
    public enum CardIntField
    {
        Atk, Def, Lvl, EquipCount
    }

    static class CardIntFieldExtensions
    {
        public static int GetValue(this CardIntField field, Card card)
        {
            var monster = card as Monster;
            return field switch
            {
                CardIntField.Atk => monster != null ? monster.Atk : 0,
                CardIntField.Def => monster != null ? monster.Def : 0,
                CardIntField.Lvl => monster != null ? monster.Lvl : 0,
                CardIntField.EquipCount => card.equips.Count,
                _ => 0,
            };
        }

        public static int GetOriginalValue(this CardIntField field, Card card)
        {
            var monster = card as Monster;
            var data = card.data.OriginalData as MonsterData;

            return field switch
            {
                CardIntField.Atk => monster != null ? data.atk : 0,
                CardIntField.Def => monster != null ? data.def : 0,
                CardIntField.Lvl => monster != null ? data.lvl : 0,
                CardIntField.EquipCount => card.equips.Count,
                _ => 0,
            };
        }

        public static IntHolder GetIntHolder(this CardIntField field, Card card)
        {
            var monster = card as Monster;
            return field switch
            {
                CardIntField.Atk => monster.AtkMod,
                CardIntField.Def => monster.DefMod,
                CardIntField.Lvl => monster.LvlMod,
                _ => null,
            };
        }
    }
}