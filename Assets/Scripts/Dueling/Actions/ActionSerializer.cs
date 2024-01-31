using System;

namespace BBG.Dueling.Actions
{
    public class ActionSerializer
    {
        static Player Player(string id) => Duel.Current.GetPlayer(int.Parse(id));

        public static DuelAction Deserialize(string data)
        {
            var d = data.Split('/');
            var a1 = d[1];
            var cards = Duel.Current.Cards;

            return d[0] switch
            {
                "CP" => new ChangePhaseAction(Enum.Parse<DuelPhase>(d[1])),
                "DC" => new DrawCardsAction(Player(a1), int.Parse(d[2]), Reason.Other),
                "CL" => new ChangeLPAction(Player(a1), int.Parse(d[2]), Enum.Parse<Reason>(d[3]), null),
                "SS" => new SetSummon(cards[a1] as Monster),
                "NS" => new NormalSummon(cards[a1] as Monster),
                "FS" => new FlipSummon(cards[a1] as Monster),
                "FP" => new FlipPositionAction(cards[a1] as Monster),
                "SC" => new SetCardAction(cards[a1]),
                "AT" => new AttackAction(cards[a1] as Monster),
                "AC" => new ActivationAction(cards[a1], int.Parse(d[2])),
                "ED" => new EndDuelAction(Enum.Parse<EndDuelAction.Reason>(d[2]), Player(a1)),
                "PS" => new ChangePositionAction(cards[a1], Enum.Parse<CardPosition>(d[2])),
                "AF" => new AncientFusionSummon(cards[a1] as Monster),
                "MS" => new ManualSpecialSummon(cards[a1] as Monster),
                _ => throw new ArgumentException()
            };
        }

        public static string Serialize(DuelAction action)
        {
            return action switch
            {
                ChangePhaseAction cp => $"CP/{cp.nextPhase}",
                DrawCardsAction dc => $"DC/{dc.player.ID}/{dc.amount}",
                ChangeLPAction cl => $"CL/{cl.player.ID}/{cl.amount}/{cl.Reason}",
                SetSummon ss => $"SS/{ss.card.id}",
                NormalSummon ns => $"NS/{ns.card.id}",
                FlipSummon fs => $"FS/{fs.card.id}",
                FlipPositionAction fp => $"FP/{fp.card.id}",
                SetCardAction sc => $"SC/{sc.card.id}",
                AttackAction at => $"AT/{at.card.id}",
                ActivationAction ac => $"AC/{ac.card.id}/{ac.card.effects.IndexOf(ac.effect)}",
                EndDuelAction ed => $"ED/{ed.winner.ID}/{ed.winReason}",
                ChangePositionAction ps => $"PS/{ps.card.id}/{ps.position}",
                AncientFusionSummon af => $"AF/{af.summoned.id}",
                ManualSpecialSummon ms => $"MS/{ms.card.id}",
                _ => throw new ArgumentException()
            };
        }
    }
}