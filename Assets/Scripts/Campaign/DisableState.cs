using UnityEngine;
using UnityEngine.SceneManagement;

namespace BBG.Campaign
{
    public class DisableState : MonoBehaviour
    {
        string ID => SceneManager.GetActiveScene().name + "/" + name + "-DISABLED";
        CampaignData CampaignData => CampaignManager.Data;

        private void Start()
        {
            if (CampaignData != null)
                UpdateState();
        }

        private void SetState(bool active)
        {
            if (active)
                CampaignData.RemoveString(ID);
            else
                CampaignData.AddString(ID);
            UpdateState();
        }

        private void UpdateState() => gameObject.SetActive(!CampaignData.HasString(ID));

        [ContextMenu("DISABLE")]
        public void Disable() => SetState(false);

        [ContextMenu("ENABLE")]
        public void Enable() => SetState(true);
    }
}