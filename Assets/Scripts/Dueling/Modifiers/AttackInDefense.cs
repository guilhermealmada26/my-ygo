using BBG.Conditions;
using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Modifiers/AttackInDefense")]
    public class AttackInDefense : Modifier
    {
        public Condition[] attackerFilter;
        public bool useAtkValue;

        protected override void OnSetup()
        {
            Events.ObserveValidate(EventName.AttackAction, OnAttack);
            Events.ObserveValidate(EventName.BattleAction, OnBattle);
        }

        internal override void BeforeRemove()
        {
            base.BeforeRemove();
            Events.RemoveObserve(EventName.AttackAction, OnAttack);
            Events.RemoveObserve(EventName.BattleAction, OnBattle);
        }

        private void OnAttack(DuelAction action)
        {
            if (card.Location != Location.MonsterZone || !card.IsFaceUp())
                return;

            var attack = action as AttackAction;
            if (!attack.card.IsValid(this, attackerFilter))
                return;

            attack.ValidPosition = true;
        }

        private void OnBattle(DuelAction action)
        {
            if (card.Location != Location.MonsterZone || !card.IsFaceUp())
                return;

            var battle = action as BattleAction;
            if (!battle.card.IsValid(this, attackerFilter))
                return;

            battle.ValidPosition = true;
            battle.DamageValue = useAtkValue ? battle.attacker.Atk : battle.attacker.Def;
        }
    }
}