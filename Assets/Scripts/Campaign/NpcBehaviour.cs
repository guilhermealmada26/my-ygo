using Sirenix.OdinInspector;
using UnityEngine;

namespace BBG.Campaign
{
    public abstract class NpcBehaviour : ScriptableObject
    {
        [SerializeField, InlineEditor] protected Character data;

        public virtual void OnStart() { }

        public virtual void OnInvoke() { }

        public Sprite Picture => data.PictureImage;
        public Sprite Img => data.DialogueImage;
    }
}