using BBG.Dueling.Replay;
using UnityEngine;

namespace BBG.Dueling.Settings
{
    [CreateAssetMenu(menuName = "Duel/Settings/ReplayConfig")]
    public class ReplayConfig : ConfigurationExtender
    {
        public ReplayData data;
        public string fileName;

        internal override void BeforeSetup(DuelConfiguration configuration, Duel duel)
        {
            configuration.startingPlayerID = data.startingPlayerId;
            configuration.player = JsonUtility.FromJson<PlayerData>(data.playerJson);
            configuration.player.control = ControlMode.Replay;
            configuration.opponent = JsonUtility.FromJson<PlayerData>(data.opponentJson);
            configuration.opponent.control = ControlMode.Replay;
            FindObjectOfType<ReplayManager>().SetData(data);
            duel.SetSeed(data.duelSeed);
        }

        [ContextMenu("LoadTestData")]
        public void LoadTestData()
        {
            var path = Application.streamingAssetsPath + fileName;
            var json = System.IO.File.ReadAllText(path);
            data = JsonUtility.FromJson<ReplayData>(json);
        }
    }
}