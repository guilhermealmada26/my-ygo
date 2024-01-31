using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

namespace BBG.Ygo.Network
{
    public class AutoNetworkedDuel : MonoBehaviourPunCallbacks
    {
        [SerializeField] CreateJoinGame manager;
        [SerializeField] GameObject onJoin;
        [SerializeField] TextMeshProUGUI message;

        private void Start()
        {
            if (message != null)
                manager.onStartingDuel += () => message.text = "Starting Duel...";
        }

        public void StartDuel()
        {
            StartCoroutine(TryJoin());
        }

        private IEnumerator TryJoin()
        {
            yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady);
            manager.JoinRoom();
        }

        public override void OnJoinedRoom()
        {
            onJoin.SetActive(true);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            manager.CreateRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            manager.CreateRoom();
        }
    }
}