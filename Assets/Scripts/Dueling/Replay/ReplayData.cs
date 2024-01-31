using BBG.Dueling.Settings;
using UnityEngine;

namespace BBG.Dueling.Replay
{
    [System.Serializable]
    public class ReplayData
    {
        public string[] declaredActions;
        public string[] selectionDatas;
        public string musicName;
        public int startingPlayerId;
        public int duelSeed;
        public string playerJson, opponentJson;

        public ReplayData(DuelConfiguration configuration, string[] declaredActions, string[] selectionDatas)
        {
            musicName = configuration.musicName;
            startingPlayerId = configuration.startingPlayerID;
            playerJson = JsonUtility.ToJson(configuration.player);
            opponentJson = JsonUtility.ToJson(configuration.opponent);
            this.declaredActions = declaredActions;
            this.selectionDatas = selectionDatas;
            duelSeed = Duel.Current.LastSeed;
        }
    }
}