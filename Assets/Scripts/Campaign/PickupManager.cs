using BBG.Ygo;
using BBG.Ygo.CardViews;
using BBG.Ygo.DeckEditing;
using System;
using UnityEngine;

namespace BBG.Campaign
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] CardView cardView;
        [SerializeField] DialogueManager dialogueManager;

        const string MONEY_PICKUP_TEXT = "You have picked <b>{0}</b> DP.";
        const string CARD_PICKUP_TEXT = "You have picked the card <color=red>\"{0}\"</color>.";

        public static PickupManager Instance { get; private set; }
        private Action onDisable;

        private void Awake()
        {
            Instance = this;
        }

        public void AddMoney(int amount, Action onDisable = null)
        {
            CampaignManager.Data.AddMoney(amount);
            var moneyText = string.Format(MONEY_PICKUP_TEXT, amount);
            dialogueManager.ShowLine(moneyText, null, null, OnClose);
            this.onDisable = onDisable;
        }

        public void AddCard(CardData card, Action onDisable = null)
        {
            DeckValuesManager.Current.AddToMallet(card);
            cardView.SetData(card);
            cardView.gameObject.SetActive(true);
            var cardText = string.Format(CARD_PICKUP_TEXT, card.cardName);
            dialogueManager.ShowLine(cardText, null, null, OnClose);
            this.onDisable = onDisable;
        }

        public void Close()
        {
            dialogueManager.Close();
        }

        void OnClose()
        {
            cardView.gameObject.SetActive(false);
            onDisable?.Invoke();
        }
    }
}