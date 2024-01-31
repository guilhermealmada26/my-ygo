using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class FieldSpellView : DuelComponent
    {
        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ActivationAction>(OnActivate);
        }

        private void OnActivate(DuelAction action)
        {
            OnEnteredField((action as ActivationAction).card);
        }

        private void OnEnteredField(Card card)
        {
            if (card.Location == Location.FieldZone)
            {
                card.location.ValueChanged += OnMoved;
                SetBackground(card.Controller, card.data.sprite);
                //set opponent field if he controls no face up field card
                var opponent = card.Controller.Opponent;
                var oppHasField = opponent[Location.FieldZone].Count > 0 && card.IsFaceUp();
                if (!oppHasField)
                    SetBackground(opponent, card.data.sprite);
            }

            void OnMoved()
            {
                card.location.ValueChanged -= OnMoved;
                OnFieldCardMoved(card);
            }
        }

        void SetBackground(Player player, Sprite sprite)
        {
            player.GetComponent<PlayerView>().SetBackgorund(sprite);
        }

        private void OnFieldCardMoved(Card card)
        {
            var oppField = card.Controller.Opponent[Location.FieldZone];
            var oppHasField = oppField.Count > 0 && oppField[0].IsFaceUp();
            Sprite sprite = null;
            if (oppHasField)
                sprite = oppField[0].data.sprite;

            SetBackground(card.Controller, sprite);
            if (!oppHasField)
            {
                SetBackground(card.Controller.Opponent, null);
            }
        }
    }
}
