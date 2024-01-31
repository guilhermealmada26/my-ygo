using BBG.Ygo.CardViews;
using UnityEngine.EventSystems;

namespace BBG.Ygo
{
    public class CardTipTrigger : CardHolder, IPointerClickHandler
    {
        private CardTipManager tipManager;

        private void Start()
        {
            tipManager = CardTipManager.Instance;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (enabled)
                tipManager.ShowOnMainTooltip(card);
        }
    }
}