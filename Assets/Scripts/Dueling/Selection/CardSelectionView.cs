using BBG.Dueling.View;
using BBG.Ygo;
using BBG.Ygo.CardViews;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BBG.Dueling.Selection.View
{
    public class CardSelectionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] CanvasGroup cardFront;
        [SerializeField] GameObject selectable, selected;
        [SerializeField] Image locationIcon, controllerOutline;

        private Card card;
        private CardsSelection selection;
        private Action<Card, bool> onClick;
        private float previousTransparency;

        public void SetData(Card card, CardsSelection selection, Action<Card, bool> onClick)
        {
            this.card = card;
            this.selection = selection;
            this.onClick = onClick;
            selectable.SetActive(true);
            GetComponent<CardView>().SetData(card.data);
            if (locationIcon != null)
                locationIcon.sprite = LocationIcons.Get(card.Location);
            if (controllerOutline != null)
                controllerOutline.color = card.Controller == selection.player ? Color.blue : Color.red;

            bool faceUp = card.IsFaceUp() || SelectionerIsOwner || selection.setAllFaceUp;
            previousTransparency = cardFront.alpha;
            cardFront.alpha = faceUp ? 1 : 0;
            GetComponent<CardTipTrigger>().enabled = faceUp;
        }

        private bool SelectionerIsOwner => card.Controller == selection.player || card.Controller.Control == ControlMode.Manual;

        public void Disable()
        {
            selectable.SetActive(false);
            selected.SetActive(false);
            cardFront.alpha = previousTransparency;
            GetComponent<CardTipTrigger>().enabled = previousTransparency == 1;
            card = null;
            selection = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (selection == null)
                return;
            onClick.Invoke(card, selected.activeInHierarchy);
            UpdateSelectedView();
        }

        internal void UpdateSelectedView()
        {
            if (!selection.Confirmed)
                selected.SetActive(selection.Selected.Contains(card));
        }
    }
}