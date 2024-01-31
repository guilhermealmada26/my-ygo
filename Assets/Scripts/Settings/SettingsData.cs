using UnityEngine;

namespace BBG.Settings
{
    [System.Serializable]
    public class SettingsData
    {
        [Range(0f, 1f)] 
        public float volume = 1.0f;
        public bool fullscreen;
    }
}