using BBG.Dueling.Settings;
using BBG.Ygo.DeckEditing;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace BBG.Ygo.Network
{
    public class CreateJoinGame : MonoBehaviourPunCallbacks
    {
        [SerializeField] DuelConfiguration networkedDuelConfig;
        [SerializeField] SceneLoader sceneLoader;

        public UnityAction onStartingDuel;
        public UnityAction onStartDuel;

        public void CreateRoom(string roomName = null)
        {
            if (PhotonNetwork.CurrentRoom != null || !PhotonNetwork.IsConnectedAndReady)
                return;

            roomName ??= System.Guid.NewGuid().ToString();
            PhotonNetwork.CreateRoom(roomName, new RoomOptions
            {
                MaxPlayers = 2,
                PublishUserId = true
            });
        }

        public void JoinRoom(string roomName = null)
        {
            if (PhotonNetwork.CurrentRoom != null || !PhotonNetwork.IsConnectedAndReady)
                return;
            if (roomName != null)
                PhotonNetwork.JoinRoom(roomName);
            else
                PhotonNetwork.JoinRandomRoom();
        }

        public void LeaveRoom()
        {
            if (PhotonNetwork.CurrentRoom != null)
                PhotonNetwork.LeaveRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            TryStartDuel();
        }

        public override void OnJoinedRoom()
        {
            SetPlayerData();
            TryStartDuel();
        }

        private void SetPlayerData()
        {
            networkedDuelConfig.player.mainDeck = DeckValuesManager.Current.equipped.MainDeck.GetIDs();
            networkedDuelConfig.player.extraDeck = DeckValuesManager.Current.equipped.ExtraDeck.GetIDs();

            var props = new Hashtable
            {
                { PlayerProperties.PLAYER_DATA, JsonUtility.ToJson(networkedDuelConfig.player) },
                { PlayerProperties.DUEL_SEED, Random.Range(int.MinValue, int.MaxValue) },
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        private void TryStartDuel()
        {
            if (PhotonNetwork.PlayerListOthers.Length < 1)
                return;

            networkedDuelConfig.SetAsOverride("Networked");
            onStartingDuel?.Invoke();
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Invoke(nameof(StartDuel), 1.5f);
        }

        private void StartDuel()
        {
            onStartDuel?.Invoke();
            sceneLoader.LoadScene("Duel");
        }
    }
}