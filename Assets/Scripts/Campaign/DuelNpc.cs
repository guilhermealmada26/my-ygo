using BBG.Ygo.DeckEditing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BBG.Campaign
{
    [CreateAssetMenu(menuName = "Campaign/DuelNpc")]
    public class DuelNpc : NpcBehaviour
    {
        [SerializeField]
        Deck deck;

        [InfoBox("New lines with enter treated as other dialogue lines")]
        [SerializeField, TextArea(3, 3)]
        string talkLines;

        [SerializeField, TextArea(2, 2)]
        string duelLines, onWin, onLose;

        private string[] options;

        DialogueManager DialogueManager => DialogueManager.Instance;
        CampaignDuelManager DuelManager => CampaignDuelManager.Instance;

        public override void OnStart()
        {
            options = new string[] { "Talk", "Duel", "Stats" };
            if (DuelManager.LastDuelStats.IsValidCharacter(data))
            {
                var npcWon = DuelManager.LastDuelStats.WonLastDuel(data);
                var lines = npcWon ? onWin : onLose;
                DialogueManager.ShowLines(lines.Split("\n"), data.ChrName, data.DialogueImage);
            }
        }

        public override void OnInvoke()
        {
            var manager = FindObjectOfType<OptionsManager>();
            manager.ShowChoices(options, (i) =>
            {
                if (i == 0)
                    DialogueManager.ShowLines(talkLines.Split("\n"), data.ChrName, data.DialogueImage);
                else if (i == 1)
                    Duel();
                else
                    Stats();
            });
        }

        void Duel()
        {
            DialogueManager.ShowLines(duelLines.Split("\n"), data.ChrName, data.DialogueImage, () => DuelManager.StartDuel(data, deck));
        }

        void Stats()
        {
            var text = DuelManager.GetStatsText(data);
            DialogueManager.ShowLine(text, data.ChrName, null);
        }
    }
}