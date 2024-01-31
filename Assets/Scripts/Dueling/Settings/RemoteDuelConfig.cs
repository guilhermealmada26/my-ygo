using BBG.Dueling.Actions;
using BBG.Dueling.Settings;
using BBG.Ygo.Network;
using Photon.Pun;
using UnityEngine;

namespace BBG.Dueling.Network
{
    [CreateAssetMenu(menuName = "Duel/Settings/RemoteDuelConfig")]
    public class RemoteDuelConfig : ConfigurationExtender
    {
        internal override void BeforeSetup(DuelConfiguration configuration, Duel duel)
        {
            if (PhotonNetwork.PlayerListOthers.Length < 1)
            {
                var action = new EndDuelAction(EndDuelAction.Reason.Disconnection, null)
                {
                    priority = Priority.AboveAll
                };
                duel.StartingActions.Add(action);
                return;
            }

            var opponent = PhotonNetwork.PlayerListOthers[0];
            var opponentData = JsonUtility.FromJson<PlayerData>((string)opponent.CustomProperties[PlayerProperties.PLAYER_DATA]);
            opponentData.control = ControlMode.Remote;
            configuration.opponent = opponentData;
            //set host synchrous data
            var host = PhotonNetwork.MasterClient;
            var hostData = JsonUtility.FromJson<PlayerData>((string)host.CustomProperties[PlayerProperties.PLAYER_DATA]);
            configuration.player.initialLP = opponentData.initialLP = hostData.initialLP;
            configuration.player.initialDrawCount = opponentData.initialDrawCount = hostData.initialDrawCount;

            //duel seed 
            var duelSeed = host.CustomProperties[PlayerProperties.DUEL_SEED];
            duel.SetSeed((int)duelSeed);
            //players ids
            duel.LocalPlayer.SetId(PhotonNetwork.IsMasterClient);
            duel.LocalOpponent.SetId(!PhotonNetwork.IsMasterClient);
        }
    }
}