using UnityEngine;

namespace BBG.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StringSO")]
    public class StringSO : ScriptableObject
    {
        [SerializeField] string text;

        public string String => text;
    }
}