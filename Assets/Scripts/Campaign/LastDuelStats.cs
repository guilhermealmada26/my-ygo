using BBG.Dueling.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBG.Campaign
{
    [CreateAssetMenu(menuName = "Campaign/LastDuelStats")]
    public class LastDuelStats : ScriptableObject
    {
        [SerializeField] DuelStatistics duelStatistics;

        public void SetData(Character character)
        {
            CampaignManager.Data.AddString(CallbackStr(character, 1));
        }

        public bool IsValidCharacter(Character character)
        {
            var callback = CallbackStr(character, 0);
            var data = CampaignManager.Data;
            if (data == null || !data.HasString(callback))
                return false;
            data.RemoveString(callback);
            return true;
        }

        public bool WonLastDuel(Character character)
        {
            var results = GetResults(character.DuelID);
            return results.Last().winnerName == character.ChrName;
        }

        string CallbackStr(Character character, int sum)
        {
            var callback = character.DuelID + "DUELCALLBACK";
            var duelCount = GetResults(character.DuelID).Count + sum;
            return callback + duelCount;
        }

        List<DuelRegistry> GetResults(string oppId)
        {
            //TODO -> consider only duels in current campaign
            return duelStatistics.GetResults().Where(r => r.duelTag == oppId).ToList();
        }
    }
}