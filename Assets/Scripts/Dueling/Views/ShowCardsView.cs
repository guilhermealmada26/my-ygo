using BBG.Dueling.Actions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Dueling.View
{
    public class ShowCardsView : DuelComponent
    {
        [SerializeField] GameObject canvas;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] ScrollCardView prefab;
        [SerializeField] int maxAmount = 50;
        [SerializeField] Scrollbar scrollbar;

        private ScrollCardView[] views;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ShowCardsAction>(ShowCards);
            views = new ScrollCardView[maxAmount];
            views[0] = prefab;
            for (int i = 1; i < views.Length; i++)
                views[i] = Instantiate(prefab, prefab.transform.parent);
        }

        private void ShowCards(DuelAction a)
        {
            var action = a as ShowCardsAction;
            if (action.IsEffect && action.player.Control != ControlMode.Manual)
                return;
            if(action.message != null)
                text.text = action.message;
            canvas.SetActive(true);
            Invoke(nameof(UpdateScroll), 0.02f);
            for (int i = 0; i < views.Length; i++)
                views[i].gameObject.SetActive(i < action.cards.Count);
            for (int i = 0; i < action.cards.Count; i++)
                views[i].SetData(action.cards[i]);
            if (action.IsEffect)
                Execute(FreezeActionsUntilConfirmed());
        }

        private void UpdateScroll()
        {
            scrollbar.value = 0;
        }

        private IEnumerator FreezeActionsUntilConfirmed()
        {
            yield return new WaitWhile(() => canvas.activeInHierarchy);
        }

        private void ShowCards(bool localPlayer, Location location)
        {
            var player = localPlayer ? duel.LocalPlayer : duel.LocalOpponent;
            var cards = player[location];
            if(cards.Count > 0 && !canvas.activeInHierarchy)
            {
                text.text = player.Data.name + "'s " + location;
                new ShowCardsAction(cards, player).ProcessPerform();
            }
        }

        public void ShowPlayerGraveyard() => ShowCards(true, Location.Graveyard);
        public void ShowPlayerBanished() => ShowCards(true, Location.Banished);
        public void ShowPlayerExtraDeck() => ShowCards(true, Location.ExtraDeck);
        public void ShowOpponentGraveyard() => ShowCards(false, Location.Graveyard);
        public void ShowOpponentBanished() => ShowCards(false, Location.Banished);
    }
}
