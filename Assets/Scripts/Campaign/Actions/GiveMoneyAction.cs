using System;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "Give Money")]
    public class GiveMoneyAction : CampaignAction
    {
        public int[] possibleMoney;

        protected override void OnInvoke(IActionContainer container)
        {
            var money = possibleMoney.GetRandom();
            PickupManager.Instance.AddMoney(money);
        }
    }
}