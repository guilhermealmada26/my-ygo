namespace BBG.Dueling.Actions
{
    public class ChangeTurnAction : DuelAction
    {
        protected override void OnPerform()
        {
            duel.TurnCount++;
            duel.CurrentPlayer = duel.CurrentPlayer.Opponent;
        }
    }
}