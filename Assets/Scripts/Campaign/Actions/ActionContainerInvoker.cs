using Sirenix.OdinInspector;
using UnityEngine;

namespace BBG.Campaign
{
    public class ActionContainerInvoker : SerializedMonoBehaviour, IActionContainer
    {
        public ActionContainer container;
        [SerializeReference, SubclassSelector(AllowDuplicates = true, DrawBoxForListElements = true)]
        public CampaignAction[] actions;

        public CampaignAction[] Actions => actions;

        public void Invoke()
        {
            if (container != null)
                container.Invoke();
            else
                actions[0].Invoke(this);
        }

        private void Start()
        {
            Invoke();
        }
    }
}