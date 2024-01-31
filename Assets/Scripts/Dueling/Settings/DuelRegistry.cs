using BBG.Dueling.Actions;

namespace BBG.Dueling.Settings
{
    [System.Serializable]
    public class DuelRegistry
    {
        public string playerName;
        public string opponentName;
        public string duelTag;
        public string winnerName;
        public EndDuelAction.Reason winReason;
        public ControlMode opponentControl;

        public DuelRegistry(Duel duel, EndDuelAction endDuel)
        {
            winnerName = endDuel.winner.Data.name;
            winReason = endDuel.winReason;
            playerName = duel.LocalPlayer.Data.name;
            opponentName = duel.LocalOpponent.Data.name;
            opponentControl = duel.LocalOpponent.Control;
            duelTag = duel.Tag;
        }
    }
}