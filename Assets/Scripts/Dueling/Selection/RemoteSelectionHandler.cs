using BBG.Dueling.Actions;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Selection
{
    public class RemoteSelectionHandler : MonoBehaviourPun
    {
        [SerializeField] GameObject overlay;

        private Queue<SelectionAction> waitingSelection = new();
        private Queue<string> receivedSelectionData = new();

        private void Start()
        {
            Events.Observe<SelectionAction>(OnSelectionPerform);
        }

        private void OnSelectionPerform(DuelAction action)
        {
            var selection = (SelectionAction)action;
            if (selection.player.Control != ControlMode.Remote)
                return;

            overlay.SetActive(true);
            waitingSelection.Enqueue(selection);
            TrySetData();
            CoroutineQueue.Instance.AddCoroutine(WaitUntilSelected());
        }

        internal bool IsWaiting => overlay.activeInHierarchy;

        private IEnumerator WaitUntilSelected()
        {
            yield return new WaitWhile(() => IsWaiting);
            //yield return new WaitForSeconds(.25f);
        }

        public void SendSelectionData(SelectionAction selection)
        {
            if (PhotonNetwork.CurrentRoom == null)
                return;

            photonView.RPC(nameof(RPC_SelectionDataReceived), RpcTarget.OthersBuffered, selection.GetLoadableData());
        }

        [PunRPC]
        private void RPC_SelectionDataReceived(string data)
        {
            receivedSelectionData.Enqueue(data);
            TrySetData();
        }

        private void TrySetData()
        {
            if (waitingSelection.Count == 0 || receivedSelectionData.Count == 0)
                return;

            var data = receivedSelectionData.Dequeue();
            var selection = waitingSelection.Dequeue();
            selection.Confirm(data);
            overlay.SetActive(false);
        }
    }
}