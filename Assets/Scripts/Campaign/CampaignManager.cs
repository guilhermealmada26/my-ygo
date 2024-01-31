using BBG.Saving;
using UnityEngine;

namespace BBG.Campaign
{
    public class CampaignManager : SaveDataHolder<CampaignData>
    {
        [SerializeField] CampaignData data;

        public static CampaignManager Instance { get; private set; }
        public static CampaignData Data => Instance.data;

        protected override void Awake()
        {
            Instance = this;
            base.Awake();
        }

        protected override void OnLoad(CampaignData data)
        {
            this.data = data;
        }

        protected override CampaignData GetDefaultData() => data;
    }
}