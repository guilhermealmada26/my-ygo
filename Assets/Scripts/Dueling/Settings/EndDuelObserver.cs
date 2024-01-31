using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Settings
{
    public abstract class EndDuelObserver : ScriptableObject
    {
        public abstract void OnDuelEnded(EndDuelAction action, Duel duel);
    }
}