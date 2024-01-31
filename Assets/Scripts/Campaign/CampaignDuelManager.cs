using BBG.Dueling.AI;
using BBG.Dueling.Settings;
using BBG.Ygo.DeckEditing;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBG.Campaign
{
    public class CampaignDuelManager : MonoBehaviour
    {
        [SerializeField] LastDuelStats lastDuelStats;
        [SerializeField] DuelStatistics duelStatistics;
        [SerializeField] SceneLoader sceneLoader;
        [SerializeField] AIDuelConfig config;
        [SerializeField] DuelConfiguration configuration;

        public static CampaignDuelManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            duelStatistics.LoadRegistries();
        }

        public void StartDuel(Character opponent, Deck opponentDeck, AIContainer ai = null)
        {
            lastDuelStats.SetData(opponent);
            if (ai != null)
                config.container = ai;
            configuration.SetAsOverride(opponent.DuelID);
            configuration.opponent.SetDeck(opponentDeck);
            configuration.opponent.imageName = opponent.PictureImage.name;
            configuration.opponent.name = opponent.ChrName;
            sceneLoader.LoadScene("Duel");
        }

        public LastDuelStats LastDuelStats => lastDuelStats;

        List<DuelRegistry> GetResults(Character opponent)
        {
            var results = duelStatistics.GetResults();
            results.Filter(r => r.duelTag == opponent.DuelID);
            return results;
        }

        public string GetStatsText(Character opponent)
        {
            var results = GetResults(opponent);
            var wins = results.Count(r => r.winnerName != opponent.ChrName);
            var losses = results.Count(r => r.winnerName == opponent.ChrName);
            return string.Format("WINS:{0} LOSSES: {1}", wins, losses);
        }
    }
}