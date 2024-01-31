using BBG.Conditions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BBG.Ygo.CardViews
{
    public class FilterView : MonoBehaviour
    {
        [SerializeField] MultipleToggleGroup types, levels, attributes, monsterTypes, limitations;
        [SerializeField] TMP_InputField cardName;
        [SerializeField] MultipleCardsView cardsView;

        private CardValuesHolderFilter filter;
        private List<CardData> originalCards;

        public void Setup(List<CardData> cards)
        {
            filter = new CardValuesHolderFilter();
            originalCards = cards;
        }

        public void Filter()
        {
            filter.SetCardTypes(types.GetActiveNames());
            filter.SetAttributes(attributes.GetActiveNames());
            filter.SetLimitations(limitations.GetActiveNames());
            filter.SetMonsterType(monsterTypes.GetActiveNames());
            filter.SetLevels(levels.GetActiveIndexes());
            filter.SetNameSubstring(cardName.text);
            var filtered = filter.GetCards(originalCards);
            cardsView.SetCards(filtered);
        }

        public void ClearFilters()
        {
            ClearToggles();
            cardName.text = string.Empty;
            filter.Reset();
            cardsView.SetCards(originalCards);
        }

        public void ClearToggles()
        {
            types.ToggleAll(false);
            levels.ToggleAll(false);
            attributes.ToggleAll(false);
            monsterTypes.ToggleAll(false);
            limitations.ToggleAll(false);
        }
    }
}