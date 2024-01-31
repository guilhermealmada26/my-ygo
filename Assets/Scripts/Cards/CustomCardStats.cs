using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo
{
    [CreateAssetMenu]
    public class CustomCardStats : SerializedScriptableObject
    {
        public Dictionary<CardData, string> customAtks;
        public Dictionary<CardData, string> customDefs;
    }
}