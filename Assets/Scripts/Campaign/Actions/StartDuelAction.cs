using BBG.Dueling.AI;
using BBG.Ygo.DeckEditing;
using System;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "Start Duel")]
    public class StartDuelAction : CampaignAction
    {
        public AIContainer ai;
        public Character opponent;
        public Deck opponentDeck;

        protected override void OnInvoke(IActionContainer container)
        {
            CampaignDuelManager.Instance.StartDuel(opponent, opponentDeck, ai);
        }
    }
}