using Sirenix.OdinInspector;
using UnityEngine;

namespace BBG.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpriteSO")]
    public class SpriteSO : ScriptableObject
    {
        [PreviewField(90, Alignment = ObjectFieldAlignment.Center)]
        [SerializeField] Sprite sprite;

        public Sprite Sprite => sprite;
    }
}