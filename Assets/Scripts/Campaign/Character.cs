using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BBG.Campaign
{
    [CreateAssetMenu(menuName = "Campaign/CharacterSO")]
    public class Character : ScriptableObject
    {
        [SerializeField] string chrName;
        [PreviewField(85)]
        [SerializeField] Sprite picture;
        [PreviewField(75)]
        [SerializeField] Sprite dialogueImage;

        public string ChrName => chrName;
        public Sprite DialogueImage => dialogueImage;
        public Sprite PictureImage => picture;
        public string DuelID => SceneManager.GetActiveScene().name + "/" + name;
    }
}