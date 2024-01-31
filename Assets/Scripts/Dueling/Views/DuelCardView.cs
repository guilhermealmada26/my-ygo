using BBG.Ygo;
using BBG.Ygo.CardViews;
using TMPro;
using UnityEngine;

namespace BBG.Dueling.View
{
    [RequireComponent(typeof(CardView))]
    public class DuelCardView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI linkText;
        [SerializeField] GameObject linkView;
        [SerializeField] CanvasGroup cardFront;

        public GameObject askActivate, hasActions, equippedMark, negatedMark;

        private PreviousCurrent<Transform> parent;
        private ScaleOnHover onHover;
        private CardTipTrigger tipTrigger;
        private CardView view;

        public Card card { get; private set; }
        public PlayerView ControllerView => card.Controller.GetComponent<PlayerView>();

        public void SetCard(Card card)
        {
            onHover = GetComponent<ScaleOnHover>();
            tipTrigger = GetComponent<CardTipTrigger>();
            view = GetComponent<CardView>();
            parent = new PreviousCurrent<Transform>(transform.parent);
            this.card = card;
            view.SetData(card.data);
            name = card.id;
            //mods
            card.NameMods.Changed += (s) => UpdateMods();
            if (card is Monster monster)
            {
                monster.AttributeMod.Changed += (s) => UpdateMods();
                monster.MTypeMod.Changed += (s) => UpdateMods();
            }
        }

        private void UpdateMods()
        {
            view.UpdateStats();
            CardTipManager.Instance.UpdateStats();
        }

        public void SetFaceup(bool faceup) => cardFront.alpha = faceup ? 1 : 0;

        internal void UpdateStats()
        {
            if (transform.parent != parent.Current)
                parent.Current = transform.parent;
            SetHologram();

            SetFaceup(card.IsFaceUp());
            transform.localEulerAngles = PositionRotation;
            onHover.enabled = card.Location == Location.Hand;
            tipTrigger.enabled = EnableTooltip();
            if (card.data is MonsterData mdata)
                mdata.ShowNumericValuesOnly = card.Location.IsField();
            view.UpdateStats();
            CardTipManager.Instance.UpdateStats();
        }

        private void SetHologram()
        {
            if (card.Location == Location.MonsterZone)
                parent.Current.GetComponent<HologramView>()?.SetActive(card, card.IsFaceUp());
            if (card.location.Previous == Location.MonsterZone)
                parent.Previous.GetComponent<HologramView>()?.SetActive(card, false);
        }

        private bool EnableTooltip()
        {
            var handOrField = card.Location.IsHandOrField();
            if (card.Controller.Control is ControlMode.Manual or ControlMode.Replay)
                return handOrField;
            return handOrField && card.IsFaceUp();
        }

        public Vector3 PositionRotation => card.InAtkPosition() ? Vector3.zero : new Vector3(0, 0, 90);

        public void SetLinkView(int chainLink)
        {
            linkView.SetActive(true);
            linkText.text = chainLink.ToString();
        }

        public void DisableLinkView() => linkView.SetActive(false);
    }
}
