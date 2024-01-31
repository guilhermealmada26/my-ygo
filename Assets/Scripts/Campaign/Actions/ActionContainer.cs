using Sirenix.OdinInspector;
using UnityEngine;

namespace BBG.Campaign
{
    [CreateAssetMenu(menuName = "Campaign/ActionContainer")]
    public class ActionContainer : SerializedScriptableObject, IActionContainer
    {
        [SerializeReference, SubclassSelector(AllowDuplicates = true, DrawBoxForListElements = true)]
        private CampaignAction[] actions;

        public CampaignAction[] Actions => actions;

        [ContextMenu("Invoke")]
        public void Invoke()
        {
            GetAction(0)?.Invoke(this);
        }

        CampaignAction GetAction(int index)
        {
            if (index < 0 || index >= actions.Length)
                return null;
            return actions[index];
        }
    }
}