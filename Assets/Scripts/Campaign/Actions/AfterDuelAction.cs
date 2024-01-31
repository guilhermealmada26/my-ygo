using System;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "After Duel")]
    public class AfterDuelAction : CampaignAction
    {
        public Character opponent;
        public ActionContainer playerWon, playerLost, invalidDuel;

        protected override void OnInvoke(IActionContainer container)
        {
            var lastDuel = CampaignDuelManager.Instance.LastDuelStats;
            if (lastDuel == null || !lastDuel.IsValidCharacter(opponent))
            {
                InvokeActions(invalidDuel);
                return;
            }
            var playerHasWon = !lastDuel.WonLastDuel(opponent);
            InvokeActions(playerHasWon ? playerWon : playerLost);
        }

        void InvokeActions(ActionContainer container)
        {
            if (container != null)
                container.Invoke();
        }
    }
}