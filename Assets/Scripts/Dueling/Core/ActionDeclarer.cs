using BBG.Dueling.Actions;
using Photon.Pun;

namespace BBG.Dueling
{
    public class ActionDeclarer : MonoBehaviourPun
    {
        public static ActionDeclarer Instance { private set; get; }

        public event System.Action<DuelAction> ActionDeclared;

        public bool OpenState => !CoroutineQueue.Instance.Executing;

        private void Awake()
        {
            Instance = this;
        }

        public void Surrender()
        {
            var player = Duel.Current.LocalPlayer;
            var action = new EndDuelAction(EndDuelAction.Reason.Surrender, player.Opponent);
            Declare(action);
        }

        public void DrawCards(int amount)
        {
            Declare(new DrawCardsAction(Duel.Current.CurrentPlayer, amount, Reason.Other));
        }

        public void Declare(DuelAction action)
        {
            //can only declare if the duel is idle
            if (!OpenState || !action.CanPerform())
                return;

            if (Duel.Current.IsNetworked)
            {
                var data = ActionSerializer.Serialize(action);
                photonView.RPC(nameof(RPC_ActionDeclared), RpcTarget.OthersBuffered, data);
            }
            action.Perform(Priority.Manual);
            ActionDeclared?.Invoke(action);
        }

        [PunRPC]
        private void RPC_ActionDeclared(string data)
        {
            var action = ActionSerializer.Deserialize(data);
            action.Perform(Priority.Manual);
            ActionDeclared?.Invoke(action);
        }
    }
}