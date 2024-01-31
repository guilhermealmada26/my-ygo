using BBG.Ygo;
using UnityEngine;

namespace BBG.Campaign
{
    public class GiveCard : Interactable
    {
        [SerializeField] CardData[] possibleCards;

        private PickupManager pickupManager => PickupManager.Instance;

        protected override void OnInvoke()
        {
            var card = possibleCards.GetRandom();
            pickupManager.AddCard(card);
        }
    }
}