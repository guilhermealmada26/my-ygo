using BBG.Animations;
using BBG.Dueling.Actions;
using BBG.Ygo;
using UnityEngine;

namespace BBG.Dueling
{
    public class Monster : Card
    {
        readonly MonsterData monsterData;

        public readonly IntHolder AtkMod, DefMod, LvlMod;
        public readonly Modifiers<MonsterAttribute> AttributeMod;
        public readonly Modifiers<MonsterType> MTypeMod;

        internal ExtraSummon LastExtraSummon;
        internal SummonAction LastSummon;

        public Monster(MonsterData data, Player player, string id) : base(data, player, id)
        {
            monsterData = base.data as MonsterData;
            AtkMod = new IntHolder(data.atk);
            AtkMod.ValueChanged += () => monsterData.atk = AtkMod.Current;
            DefMod = new IntHolder(data.def);
            DefMod.ValueChanged += () => monsterData.def = DefMod.Current;
            LvlMod = new IntHolder(data.lvl, 1);
            LvlMod.ValueChanged += () => monsterData.lvl = LvlMod.Current;
            location.ValueChanged += OnLocationChanged;
            AttributeMod = new Modifiers<MonsterAttribute>(monsterData.attribute);
            AttributeMod.Changed += (a) => monsterData.attribute = a;
            MTypeMod = new Modifiers<MonsterType>(monsterData.monsterType);
            MTypeMod.Changed += (t) => monsterData.monsterType = t;
        }

        public int Atk => AtkMod.Current;
        public int Def => DefMod.Current;
        public int Lvl => LvlMod.Current;
        public Sprite Hologram => monsterData.hologram;
        public ParticleAnimation AtkAnimation => monsterData.AttackAnimation;

        public int PointsByPosition => Position.InAtkPosition() ? Atk : Def;

        void OnLocationChanged()
        {
            if (location.Previous == Location.MonsterZone && location.Current != Location.MonsterZone)
            {
                AtkMod.Reset();
                DefMod.Reset();
                LvlMod.Reset();
            }
        }
    }
}