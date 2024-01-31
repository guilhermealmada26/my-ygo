using System;
using UnityEngine;

namespace BBG.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] SettingsData data;
        
        public SettingsData Data => data;
        
        public event Action OnSetData;

        public void SetValues(SettingsData _data)
        {
            data.volume = _data.volume;
            data.fullscreen = _data.fullscreen;
            UpdateGameValues();
            OnSetData?.Invoke();
        }    

        public void UpdateGameValues()
        {
            AudioListener.volume = data.volume;
            //Screen.fullScreen = data.fullscreen;
        }
    }
}