using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Settings
{
    [RequireComponent(typeof(SettingsSaveSetup))]
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] GameObject menuPanel;
        [SerializeField] Slider volumeSl = null;
        [SerializeField] Toggle tgFullscreen = null;

        private SettingsManager settingsManager;
        private SettingsData data;

        private void Awake()
        {
            settingsManager = GetComponent<SettingsManager>();
            settingsManager.OnSetData += UpdateUI;
            data = settingsManager.Data;
            Enable(true);
        }

        public void Enable(bool value)
        {
            menuPanel.SetActive(value);
        }

        private void UpdateUI()
        {
            volumeSl.value = data.volume;
            tgFullscreen.isOn = data.fullscreen;
        }

        public void UpdateValues()
        {
            data.volume = volumeSl.value;
            data.fullscreen = tgFullscreen.isOn;
            settingsManager.UpdateGameValues();
        }
    }
}