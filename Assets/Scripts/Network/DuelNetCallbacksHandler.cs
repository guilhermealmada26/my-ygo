using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using Photon.Pun;

namespace BBG.Ygo.Network
{
    public class DuelNetCallbacksHandler : MonoBehaviourPunCallbacks
    {
        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            var remoteSelection = FindObjectOfType<RemoteSelectionHandler>();
            if (remoteSelection.IsWaiting)
            {
                Invoke(nameof(DisconnectImmediate), 0.25f);
            }
            else
            {
                new EndDuelAction(EndDuelAction.Reason.Disconnection, null).Perform(Priority.AboveAll);
            }
        }

        private void DisconnectImmediate()
        {
            new EndDuelAction(EndDuelAction.Reason.Disconnection, null).ProcessPerform();
        }
    }
}