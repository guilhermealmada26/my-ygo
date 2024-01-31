using BBG.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Dueling.View
{
    public class PlayerPortrait : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] SpriteCollection imageCollection;
        [SerializeField] Image image;
        [SerializeField] GameObject highlight;
        [SerializeField] TextMeshProUGUI lpText, damageText;

        public void SetData(Player player)
        {
            playerName.text = player.Data.name;
            image.sprite = imageCollection.Get(player.Data.imageName);
            SetLp(player.LP);
        }

        public void Highlight(bool yes) => highlight.SetActive(yes);

        public void SetLp(int lp) => lpText.text = lp.ToString();

        public void SetDamageText(string value) => damageText.text = value;

        public void TriggerHeal() => animator.SetTrigger("Heal");
        
        public void TriggerDamage() => animator.SetTrigger("Damage");

        public bool IsAnimating => animator.IsAnimating();

        public TextMeshProUGUI LpText => lpText;
    }
}
