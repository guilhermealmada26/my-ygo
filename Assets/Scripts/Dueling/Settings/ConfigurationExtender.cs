using UnityEngine;

namespace BBG.Dueling.Settings
{
    public abstract class ConfigurationExtender : ScriptableObject
    { 
        internal virtual void BeforeSetup(DuelConfiguration configuration, Duel duel) { }

        internal virtual void Setup(Duel duel) { }
    }
}