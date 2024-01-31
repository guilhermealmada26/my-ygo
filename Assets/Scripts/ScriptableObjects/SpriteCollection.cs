using Sirenix.OdinInspector;
using UnityEngine;

namespace BBG.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpriteCollection")]
    public class SpriteCollection : ScriptableObject
    {
        [SerializeField]
        [PreviewField(40)]
        Sprite[] sprites;

        public Sprite[] Assets { get => sprites; set => sprites = value; }

        public Sprite Get(string name)
        {
            foreach (var so in Assets)
                if (so.name == name)
                    return so;
            return null;
        }
    }
}