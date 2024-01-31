using BBG.Dueling.Selection;
using BBG.OptionsSelection;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Actions
{
    public class NormalSummon : SummonAction
    {
        internal readonly List<int> TributeAmounts;
        internal List<Card> Tributed { private set; get; }

        public NormalSummon(Monster monster) : base(monster.Controller, monster)
        {
            position = CardPosition.AtkFaceUp;
            TributeAmounts = new List<int> { TributeCount() };
        }

        private int TributeCount()
        {
            if (Monster.Lvl < 5)
                return 0;
            if (Monster.Lvl < 7)
                return 1;
            return 2;
        }

        protected override bool ValidPerform()
        {
            if (Monster.data.type.subtype == Ygo.Subtype.Ritual)
                return false;
            var summons = TurnActionsRegistry.Instance.PerformedThisTurn<NormalSummon>();
            summons.Filter(s => s.player == player);
            if (summons.Count == player.MaxSummonsPerTurn)
                return false;
            if (priority == Priority.Manual)
            {
                if (Monster.Location != Location.Hand)
                    return false;
                if (duel.Phase != DuelPhase.Main)
                    return false;
            }
            return TributeAmounts.Any(a => IsValid(a));
        }

        private bool IsValid(int tributeAmount)
        {
            if (tributeAmount > 0)
            {
                var selection = new CardsSelection(player, GetTributable(), (uint)tributeAmount)
                {
                    CallerObject = this
                };
                if (!selection.CanPerform())
                    return false;
            }
            return !player.IsFull(Location.MonsterZone, 1 - tributeAmount);
        }

        private List<Card> GetTributable()
        {
            var cards = new List<Card>(player[Location.MonsterZone]);
            cards.Filter((c) => new TributeAction(c, Reason.Other, player).CanPerform());
            return cards;
        }

        protected override void BeforePerform()
        {
            new DelegateAction(TryTributeCards).Perform();
        }

        private void TryTributeCards()
        {
            TributeAmounts.Filter(a => IsValid(a));
            if (TributeAmounts.Count < 2)
                TributeMonsters(TributeAmounts[0]);
            else
            {
                var options = new Option[TributeAmounts.Count];
                for (int i = 0; i < options.Length; i++)
                    options[i] = new Option
                    {
                        text = $"Tribute {TributeAmounts[i]} monsters.",
                    };
                var selection = new OptionSelection(player, this is SetSummon ? "Normal Summon" : "Set Summon", options);
                selection.Perform();
                selection.AfterSelection += (s) => TributeMonsters(TributeAmounts[selection.choice.Value]);
            }
        }

        private void TributeMonsters(int tributeCount)
        {
            if (tributeCount == 0)
                return;
            var selection = new CardsSelection(player, GetTributable(), (uint)tributeCount)
            {
                isOptional = true,
                CallerObject = this
            };
            selection.Perform();
            selection.AfterSelection += (s) =>
            {
                if (selection.Cancelled)
                {
                    Cancel();
                    return;
                }
                Tributed = selection.Selected;
                Tributed.Foreach(c => new TributeAction(c, Reason.Other, player).Perform());
            };
        }
    }
}