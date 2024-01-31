using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Actions
{
    public class BattleAction : CardAction
    {
        public readonly Monster attacker;
        public Monster attacked;
        public readonly List<ChangeLPAction> damages = new(2);
        public readonly List<CardAction> destroys = new(2);

        public int DamageValue { internal set; get; }
        public Player DamagedPlayer { internal set; get; }
        public bool ValidPosition { internal set; get; }

        public bool IsDirectAttack => attacked == null;

        private Monster winner;

        public BattleAction(Monster attacker, Monster attacked) : base(attacker)
        {
            Caller = attacker;
            this.attacker = attacker;
            this.attacked = attacked;
        }

        protected override void SetData()
        {
            ValidPosition = attacker.InAtkPosition();
            DamageValue = attacker.PointsByPosition;
            if (IsDirectAttack)
            {
                winner = attacker;
                DamagedPlayer = attacker.Controller.Opponent;
            }
            else
            {
                MonsterBattle();
            }
        }

        protected override bool ValidPerform()
        {
            if (attacker.Location != Location.MonsterZone || !attacker.IsFaceUp() || !ValidPosition)
                return false;
            if (attacked != null && attacked.Location != Location.MonsterZone)
                return false;
            return base.ValidPerform();
        }

        protected override void BeforePerform()
        {
            if (DamagedPlayer != null)
                damages.Add(new ChangeLPAction(DamagedPlayer, Mathf.Abs(DamageValue), Reason.Battle, winner));
        }

        private void MonsterBattle()
        {
            DamageValue = attacker.PointsByPosition - attacked.PointsByPosition;
            if (DamageValue > 0)
            {
                AddDestroyed(attacked, attacker);
                winner = attacker;
                if (attacked.InAtkPosition())
                    DamagedPlayer = attacked.Controller;
            }
            else if (DamageValue < 0)
            {
                if (attacked.InAtkPosition())
                    AddDestroyed(attacker, attacked);
                DamagedPlayer = attacker.Controller;
                winner = attacked;
            }
            else if (attacked.InAtkPosition())
            {
                AddDestroyed(attacked, attacker);
                AddDestroyed(attacker, attacked);
            }
        }

        private void AddDestroyed(Card destroyed, Card caller)
        {
            destroys.Add(new DestroyAction(destroyed, Reason.Battle, caller));
        }

        protected override void OnPerform()
        {
            if (!ValidPerform())
            {
                Cancel();
                return;
            }
            damages.Foreach(a => a.Perform());
            new DamageCalculation(this).Perform();
            //effects may be triggered by damage effects
            new DelegateAction(() => destroys.Foreach(a => a.Perform())).Perform(Priority.AfterTriggers);
        }

        public override IEnumerable<Card> GetCards() => new Card[1] { attacked };
    }
}