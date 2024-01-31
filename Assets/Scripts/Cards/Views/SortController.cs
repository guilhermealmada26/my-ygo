using UnityEngine;
using UnityEngine.UI;

namespace BBG.Ygo.CardViews
{
    public class SortController : MonoBehaviour
    {
        [SerializeField] MultipleCardsView[] views;
        [SerializeField] GameObject panel;
        [SerializeField] Transform buttonsParent;

        Button[] buttons;
        Button currentButton;

        void Start()
        {
            buttons = buttonsParent.GetComponentsInChildren<Button>();
            ByCardType(false);
        }

        void SetSort(bool reverse, CardComparer.Criteria criteria)
        {
            foreach (var view in views)
            {
                view.Comparer.sortCriteria = criteria;
                view.SortCards(reverse);
            }
            ChangeButton((int)criteria * 2 + (reverse ? 0 : 1));
            panel.SetActive(false);
        }

        void ChangeButton(int index)
        {
            if (currentButton != null)
                currentButton.interactable = true;
            currentButton = buttons[index];
            currentButton.interactable = false;
        }

        public void ByCardType(bool descending) => SetSort(descending, CardComparer.Criteria.CardType);

        public void ByName(bool descending) => SetSort(descending, CardComparer.Criteria.Name);

        public void ByAtk(bool descending) => SetSort(descending, CardComparer.Criteria.Atk);

        public void ByDef(bool descending) => SetSort(descending, CardComparer.Criteria.Def);

        public void ByLvl(bool descending) => SetSort(descending, CardComparer.Criteria.Lvl);

        public void ByAttribute(bool descending) => SetSort(descending, CardComparer.Criteria.Attribute);

        public void ByMonsterType(bool descending) => SetSort(descending, CardComparer.Criteria.MType);
    }
}