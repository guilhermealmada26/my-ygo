using BBG.Ygo;
using System;
using UnityEngine;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "Give Card")]
    public class GiveCardAction : CampaignAction
    {
        [SerializeField] CardData[] possibleCards;

        private PickupManager pickupManager => PickupManager.Instance;

        protected override void OnInvoke(IActionContainer c)
        {
            var card = possibleCards.GetRandom();
            pickupManager.AddCard(card, () => InvokeNext(c));
        }
    }
}