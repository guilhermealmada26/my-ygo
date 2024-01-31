using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Ygo.CardViews
{
    public class CardTip : MonoBehaviour
    {
        [SerializeField] CardView cardView;
        [SerializeField] TextMeshProUGUI cardNameTx, levelTx, attributeTx, descriptionTx;
        [SerializeField] Image levelIm, attributeIm;
        [SerializeField] Sprite spellIcon, trapIcon, lvlSprite;
        [SerializeField] Color mainTagsColor, archetypeColor, modifiedColor;

        public CardView View => cardView;

        public void SetCard(CardData card)
        {
            gameObject.SetActive(true);
            cardView.SetData(card);
            cardNameTx.text = card.cardName;
            var customName = card.OriginalData.cardName != card.cardName;
            cardNameTx.color = customName ? modifiedColor : Color.white;
            descriptionTx.text = GetDescription(card);
            if (card is MonsterData monster)
                SetMonsterData(monster);
            else
                SetSpellTrapData(card);
        }

        private string GetDescription(CardData card)
        {
            var main = card.type.mainType.ToString();
            if (card is MonsterData monster)
                main = monster.monsterType.typeName;

            var c1 = ColorUtility.ToHtmlStringRGB(mainTagsColor);
            return $"<b><color=#{c1}>[{main}/{card.type.subtype}]</color></b>" +
                $"\n{card.description}";
        }

        private void SetMonsterData(MonsterData monster)
        {
            levelTx.text = monster.lvl.ToString();
            levelIm.sprite = lvlSprite;
            var customLvl = (monster.OriginalData as MonsterData).lvl != monster.lvl;
            levelTx.color = customLvl ? modifiedColor : Color.white;
            attributeTx.text = monster.attribute.attributeName;
            var customAtr = (monster.OriginalData as MonsterData).attribute != monster.attribute;
            attributeTx.color = customAtr ? modifiedColor : Color.white;
            attributeIm.gameObject.SetActive(true);
            attributeIm.sprite = monster.attribute.Sprite;
        }

        private void SetSpellTrapData(CardData card)
        {
            levelTx.text = card.type.mainType.ToString();
            levelIm.sprite = card.type.mainType == MainType.Spell ? spellIcon : trapIcon;
            levelTx.color = Color.white;
            attributeIm.gameObject.SetActive(false);
            attributeTx.text = card.type.subtype.ToString();
        }
    }
}