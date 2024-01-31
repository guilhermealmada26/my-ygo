using UnityEngine;

namespace BBG.Campaign
{
    public class GiveMoney : Interactable
    {
        [SerializeField] int[] possibleMoney;

        protected override void OnInvoke()
        {
            var money = possibleMoney.GetRandom();
            PickupManager.Instance.AddMoney(money);
        }
    }
}