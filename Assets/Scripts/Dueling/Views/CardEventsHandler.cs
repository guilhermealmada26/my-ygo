using UnityEngine;
using UnityEngine.EventSystems;

namespace BBG.Dueling.View
{
    public class CardEventsHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] CanvasGroup cardFront;

        ActionButtonsPopup buttonsPopup;
        DeclarableActionsSystem actionsSystem;
        Card _card;

        private void Awake()
        {
            buttonsPopup = FindObjectOfType<ActionButtonsPopup>();
            actionsSystem = FindObjectOfType<DeclarableActionsSystem>();
        }

        public Card card
        {
            get
            {
                if (_card == null)
                    _card = GetComponent<DuelCardView>().card;
                return _card;
            }
        }

        private Duel duel => Duel.Current;

        public void OnPointerClick(PointerEventData eventData)
        {
            var available = actionsSystem.AvailableActions;
            if (!available.ContainsKey(card))
                return;

            buttonsPopup.ClearActions();
            foreach (var action in available[card])
                buttonsPopup.AddAction(action.GetType().Name, action.Declare);

            var pos = Camera.main.WorldToScreenPoint(card.GetView().transform.position);
            var firstPlayer = duel.CurrentPlayer == duel.LocalPlayer;
            pos.y += firstPlayer ? 110 : 70;
            var scale = firstPlayer ? .6f : .45f;
            buttonsPopup.Show(pos, scale);
        }

        private bool ModifyTransparency => !card.IsFaceUp() && card.Location.IsField() && card.Controller.Control == ControlMode.Manual;

        public void OnPointerEnter(PointerEventData ev)
        {
            if (ModifyTransparency)
                cardFront.alpha = .5f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (ModifyTransparency)
                cardFront.alpha = 0;
        }
    }
}
