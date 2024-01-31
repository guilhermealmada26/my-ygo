using BBG.Saving;
using UnityEngine;

namespace BBG.Settings
{
    [RequireComponent(typeof(SettingsManager))]
    public class SettingsSaveSetup : MonoBehaviour
    {
        [SerializeField]
        private SaveManager saveManager;

        private SettingsManager settingsManager;

        private void Start()
        {
            settingsManager = GetComponent<SettingsManager>();
            var save = saveManager.Load<SettingsData>();
            if (save == null)
                return;

            settingsManager.SetValues(save);
        }

        [ContextMenu("SAVE")]
        public void SaveFile()
        {
            saveManager.Save(settingsManager.Data);
        }
    }
}