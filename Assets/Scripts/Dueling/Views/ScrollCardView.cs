using BBG.Ygo.CardViews;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BBG.Dueling.View
{
    public class ScrollCardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image locationIcon, controllerOutline;
        [SerializeField] GameObject hasActions, panelParent;

        private Card card;
        private ActionButtonsPopup popup;
        private DeclarableActionsSystem system;

        public void SetData(Card card)
        {
            this.card = card;
            GetComponent<CardView>().SetData(card.data);
            locationIcon.sprite = LocationIcons.Get(card.Location);
            var localPlayer = Duel.Current.LocalPlayer;
            controllerOutline.color = card.Controller == localPlayer ? Color.blue : Color.red;
            system = system != null ? system : FindObjectOfType<DeclarableActionsSystem>();
            hasActions.SetActive(system.AvailableActions.ContainsKey(card));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!system.AvailableActions.ContainsKey(card))
                return;

            popup = popup != null ? popup : FindObjectOfType<ActionButtonsPopup>();
            popup.ClearActions();
            foreach (var action in system.AvailableActions[card])
                popup.AddAction(action.GetType().Name, action.Declare);
            popup.Show(transform.position + new Vector3(0, 70), .8f, () => panelParent.SetActive(false));
        }

    }
}
