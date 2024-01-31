using BBG.Ygo.CardViews;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Ygo.DeckEditing
{
    public class DeckEditorController : MonoBehaviour
    {
        [SerializeField] MultipleCardsView mainDeckView, extraDeckView, malletView;
        [SerializeField] TextMeshProUGUI mainDeckCount, extraDeckCount;
        [SerializeField] FilterView malletFilter;
        [SerializeField] TMP_InputField deckName;
        [SerializeField] AlertMessage alertMessage;
        [SerializeField] CardView tooltipCardview;
        [SerializeField] Image deckImage;

        DeckEditor deckEditor;

        public Deck EditingDeck { internal set; get; }
        public List<CardData> Mallet { internal set; get; }

        const string confirmClearDeck = "Are you sure you want to clear the deck?";
        const string changedImageMessage = "Deck image changed to {0}";
        const string changedImageError = "Card must be in deck!";

        void Start()
        {
            var values = DeckValuesManager.Current;
            var deck = DeckValuesManager.GetDeckToEdit();
            Mallet ??= values.mallet;
            if (EditingDeck == null)
                SetDeck(deck != null ? deck : values.equipped);
            else
                SetDeck(EditingDeck);
        }

        void SetDeck(Deck deck)
        {
            EditingDeck = deck;
            deckEditor = new DeckEditor(Mallet, deck);

            var cardViews = FindObjectsOfType<CardMovement>();
            foreach (var cardView in cardViews)
                cardView.SetEditor(deckEditor);

            malletFilter.Setup(deckEditor.Mallet);
            UpdateUI();
            deckName.text = deck.name;

            deckEditor.CardsChanged += UpdateUI;
        }

        private void UpdateUI()
        {
            mainDeckView.SetCards(deckEditor.MainDeck);
            extraDeckView.SetCards(deckEditor.ExtraDeck);
            malletFilter.Filter();
            mainDeckCount.text = deckEditor.MainDeck.Count.ToString();
            extraDeckCount.text = deckEditor.ExtraDeck.Count.ToString();
            deckImage.sprite = EditingDeck.MainCard == null ? null : EditingDeck.MainCard.sprite;
        }

        public void ClearDeck() => alertMessage.Confirm(confirmClearDeck, deckEditor.ClearDeck);

        public void SetDeckImage()
        {
            var card = tooltipCardview.card;
            if (card == null)
                return;

            if (!EditingDeck.Contains(card))
            {
                alertMessage.ShowMessage(changedImageError);
                return;
            }

            EditingDeck.MainCard = card;
            var cardName = $"<color=red>\"{card.cardName}\"</color>";
            alertMessage.ShowMessage(string.Format(changedImageMessage, cardName));
            UpdateUI();
        }

        public void SetDeckName(string name)
        {
            DeckValuesManager.TryRename(EditingDeck, name);
        }
    }
}
