using UnityEngine;

namespace BBG.Dueling.Settings
{
    [CreateAssetMenu(menuName = "Duel/Settings/DuelConfiguration")]
    public class DuelConfiguration : ScriptableObject
    {
        public string musicName;
        [Range(-1, 1)]
        public int startingPlayerID = -1;
        public string duelTag;
        public PlayerData player;
        public PlayerData opponent;
        public ConfigurationExtender[] extenders;

        public void SetAsOverride(string duelTag)
        {
            this.duelTag = duelTag;
            DuelInitializer.Configuration = this;
        }

        public void Setup(Duel duel)
        {
            duel.Tag = duelTag;

            foreach (var ex in extenders)
                ex.BeforeSetup(this, duel);

            player.SetData(duel.LocalPlayer);
            opponent.SetData(duel.LocalOpponent);
            var id = startingPlayerID == -1 ? duel.Random.Next(0, 2) : startingPlayerID;
            duel.CurrentPlayer = duel.GetPlayer(id);

            foreach (var ex in extenders)
                ex.Setup(duel);
        }
    }
}