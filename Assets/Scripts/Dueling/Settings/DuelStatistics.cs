using BBG.Dueling.Actions;
using BBG.Saving;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Settings
{
    [CreateAssetMenu(menuName = "Duel/Settings/DuelStatistics")]
    public class DuelStatistics : EndDuelObserver
    {
        [SerializeField] SaveManager saveManager;

        private List<DuelRegistry> registries;

        public void LoadRegistries()
        {
            var save = saveManager.Load<Registries>();
            if(save == null)
            {
                save = new Registries { registries = new List<DuelRegistry>() };
                saveManager.Save(save);
            }
            registries = save.registries;
        }

        public List<DuelRegistry> GetResults()
        {
            if (registries == null)
                LoadRegistries();
            return registries; 
        }

        public override void OnDuelEnded(EndDuelAction action, Duel duel)
        {
            if (!action.DuelWasComplete || action.winner == null || duel.LocalPlayer.Control == ControlMode.Replay)
                return;

            var registries = GetResults();
            registries.Add(new DuelRegistry(duel, action));
            saveManager.Save(new Registries { registries = registries });
        }

        [System.Serializable]
        class Registries
        {
            public List<DuelRegistry> registries = new();
        }
    }
}