using BBG.Dueling.Actions;
using BBG.Dueling.Replay;
using BBG.Dueling.Settings;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class EndDuelController : DuelComponent
    {
        [SerializeField] GameObject panel;
        [SerializeField] TextMeshProUGUI mainMessage;
        [SerializeField] TextMeshProUGUI reasonMessage;
        [SerializeField] EndDuelObserver[] endDuelObservers;

        const string lifeEnded = "{0} won because {1}'s life points reached 0.";
        const string cardsEnded = "{0} won because {1} has no cards left to draw.";
        const string otherDisconnected = "The other player has disconnected from the duel.";
        const string surrendered = "{0} won because {1} has surrendered.";
        const string cardEffect = "{0} won because of {1}'s effect.";
        const string replayEnded = "Replay has ended.";

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<EndDuelAction>(DuelEnded);
        }

        private void DuelEnded(DuelAction action)
        {
            var endDuel = action as EndDuelAction;
            panel.SetActive(true);
            mainMessage.text = GetWinText(endDuel);
            reasonMessage.text = GetReasonText(endDuel);
            ActionProcessor.Instance.Process(FreezeActions, int.MaxValue);
            endDuelObservers.Foreach(o => o.OnDuelEnded(endDuel, duel));
        }

        private void FreezeActions()
        {
            FindObjectOfType<ActionRecorder>().SaveLastDuel();
            ActionProcessor.Instance.Wait = () => true;
            if (PhotonNetwork.CurrentRoom != null)
                PhotonNetwork.LeaveRoom();
        }

        private string GetWinText(EndDuelAction action)
        {
            if (action.winner == null)
                return "Duel aborted";
            if (action.winner == duel.LocalPlayer)
                return "You Win";
            return "You Lose";
        }

        private string GetReasonText(EndDuelAction action)
        {
            if (action.winReason == EndDuelAction.Reason.Disconnection)
                return otherDisconnected;
            if (action.winReason == EndDuelAction.Reason.ReplayEnded)
                return replayEnded;

            var winner = action.winner;
            var opponent = winner.Opponent;

            if (action.winReason == EndDuelAction.Reason.LifeEnded)
                return string.Format(lifeEnded, winner.Data.name, opponent.Data.name);
            if (action.winReason == EndDuelAction.Reason.CardsEnded)
                return string.Format(cardsEnded, winner.Data.name, opponent.Data.name);
            if (action.winReason == EndDuelAction.Reason.Surrender)
                return string.Format(surrendered, winner.Data.name, opponent.Data.name);

            return string.Format(cardEffect, winner.Data.name, action.card.data.cardName);
        }
    }
}
