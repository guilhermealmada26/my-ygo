using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Ygo.CardViews
{
    public class CardView : CardHolder
    {
        [Title("Card Stats")]
        [SerializeField] TextMeshProUGUI txtName;
        [SerializeField] Image image, template, limitation;
        [Title("Monster Properties")]
        [SerializeField] GameObject monsterProperties;
        [SerializeField] Image attribute;
        [SerializeField] Transform levels;
        [SerializeField] TextMeshProUGUI atk, def;
        [SerializeField] Color higherStat, lowerStat;

        private MonsterData monster;
        private Color defaultStat = Color.black;

        protected override void OnDataChanged()
        {
            monster = card as MonsterData;
            txtName.text = card.cardName;
            txtName.color = card.type.textColor;
            template.sprite = card.type.Sprite;
            image.sprite = card.sprite;
            UpdateMonsterStats();
            if (limitation != null)
                limitation.sprite = card.limitation.Sprite;
        }

        protected override void OnUpdateStats()
        {
            OnDataChanged();
        }

        private void UpdateMonsterStats()
        {
            monsterProperties.SetActive(monster != null);
            if (monster == null)
                return;

            var original = monster.OriginalData as MonsterData;
            attribute.sprite = monster.attribute.Sprite;
            SetStat(atk, monster.atk, original.atk, original.AtkText());
            SetStat(def, monster.def, original.def, original.DefText());
            for (int i = 0; i < levels.childCount; i++)
                levels.GetChild(i).gameObject.SetActive(i < monster.lvl);
        }

        private void SetStat(TextMeshProUGUI stat, int current, int original, string txt)
        {
            if (monster.ShowNumericValuesOnly.HasValue && monster.ShowNumericValuesOnly.Value)
            {
                //Debug.Log(name + "/" + monster.name);
                stat.text = current.ToString();
                stat.color = defaultStat;
                if (current > original)
                    stat.color = higherStat;
                else if (current < original)
                    stat.color = lowerStat;
            }
            else
            {
                stat.text = txt;
                stat.color = defaultStat;
            }
        }
    }
}