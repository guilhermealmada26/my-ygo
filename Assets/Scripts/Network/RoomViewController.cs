using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Ygo.Network
{
    [RequireComponent(typeof(CreateJoinGame))]
    public class RoomViewController : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_InputField ifGameId;
        [SerializeField] Toggle useManualId;
        [SerializeField] GameObject waitingOpponentCanvas;
        [SerializeField] TextMeshProUGUI waitingMessage;
        [SerializeField] Button create, join, leave;

        private CreateJoinGame createJoin;

        private void Start()
        {
            createJoin = GetComponent<CreateJoinGame>();
            createJoin.onStartingDuel += BeforeDuelStart;
            create.onClick.AddListener(CreateGame);
            join.onClick.AddListener(JoinGame);
            leave.onClick.AddListener(Leave);
        }

        private void SetButtons(bool on)
        {
            create.interactable = on;
            join.interactable = on;
        }

        public void CreateGame()
        {
            SetButtons(false);
            string roomName = useManualId.isOn ? ifGameId.text : null;
            createJoin.CreateRoom(roomName);
        }

        public void JoinGame()
        {
            SetButtons(false);
            string roomName = useManualId.isOn ? ifGameId.text : null;
            createJoin.JoinRoom(roomName);
        }

        public void Leave()
        {
            SetButtons(true);
            createJoin.LeaveRoom();
        }

        public override void OnJoinedRoom()
        {
            waitingOpponentCanvas.SetActive(true);
        }

        public override void OnLeftRoom()
        {
            waitingOpponentCanvas.SetActive(false);
        }

        private void BeforeDuelStart()
        {
            waitingMessage.text = "Starting duel...";
            leave.gameObject.SetActive(false);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            SetButtons(true);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            SetButtons(true);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            SetButtons(true);
        }
    }
}