using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Ygo.DeckEditing
{
    public class DeckSlot : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] GameObject selectedMark, equippedMark, invalidMark;
        [SerializeField] TextMeshProUGUI deckName, cardsCount, deckDetails;

        public Deck Deck { private set; get; }

        public void SetDeck(Deck deck, List<CardData> mallet)
        {
            Deck = deck;
            deckName.text = deck.name;
            image.sprite = deck.MainCard == null ? null : deck.MainCard.sprite;
            cardsCount.text = deck.MainDeck.Count.ToString();
            invalidMark.SetActive(!deck.IsValid(mallet));
        }

        internal void OnSelect()
        {
            deckDetails.text = GetDetails();
            selectedMark.SetActive(true);
        }

        internal void OnDeselect()
        {
            selectedMark.SetActive(false);
        }

        internal void SetEquipped(bool equipped)
        {
            equippedMark.SetActive(equipped);
        }

        private string GetDetails()
        {
            var str = $"<b><size=14>{Deck.name.ToUpper()}</size></b>\n\n";
            var monsters = Deck.MainDeck.Where(c => c.IsMonster && !c.GoesToExtraDeck).ToArray();
            var spells = Deck.MainDeck.Where(c => c.type.mainType == MainType.Spell).ToArray();
            var traps = Deck.MainDeck.Where(c => c.type.mainType == MainType.Trap).ToArray();

            if (monsters.Length > 0)
            {
                str += "\t<b>Monsters</b>\n\n";
                str += DuplicateCounter<CardData>.GetDuplicatesString(monsters, c => c.cardName);
            }
            if (spells.Length > 0)
            {
                str += "\n\t<b>Spells</b>\n\n";
                str += DuplicateCounter<CardData>.GetDuplicatesString(spells, c => c.cardName);
            }
            if (traps.Length > 0)
            {
                str += "\n\t<b>Traps</b>\n\n";
                str += DuplicateCounter<CardData>.GetDuplicatesString(traps, c => c.cardName);
            }
            if (Deck.ExtraDeck.Count > 0)
            {
                str += "\n\t<b>Extra</b>\n\n";
                str += DuplicateCounter<CardData>.GetDuplicatesString(Deck.ExtraDeck, c => c.cardName);
            }

            return str;
        }
    }
}