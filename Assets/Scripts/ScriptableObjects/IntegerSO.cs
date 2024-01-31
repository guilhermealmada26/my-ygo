using UnityEngine;

namespace BBG.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/IntegerSO")]
    public class IntegerSO : ScriptableObject
    {
        [SerializeField] int value;

        public int Value { get => value; set => this.value = value; }
    }
}