using BBG.Animations;
using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class ShuffleView : DuelComponent
    {
        [SerializeField] AudioClip deckShuffle, handShuffle;
        [SerializeField] float deckShuffleDuration, handShuffleDuration;
        [SerializeField] int shuffleCount = 4;
        [SerializeField] Vector3 offset;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ShuffleAction>(OnShuffle);
        }

        [ContextMenu("TEST")]
        private void Test() => new ShuffleAction(currentPlayer, Location.Deck).Declare();

        private void OnShuffle(DuelAction action)
        {
            var shuffle = action as ShuffleAction;
            if (!shuffle.showAnimation)
                return;

            if (shuffle.location == Location.Hand)
                Execute(HandAnimation(shuffle.player));
            else if (shuffle.location == Location.Deck)
                Execute(ShuffleStack(shuffle.player, shuffle.location));
        }

        private IEnumerator HandAnimation(Player player)
        {
            var hand = player.GetComponent<PlayerView>().GetLocation(Location.Hand);
            var initialScale = hand.transform.localScale;
            var toZero = new Vector3(0, initialScale.y);
            yield return ScaleCoroutine.Play(handShuffleDuration, hand.transform, toZero);
            //remove cards from transform
            foreach (Transform card in hand.transform)
                hand.RemoveChild(card);
            //add cards again in the new order
            foreach (var card in player[Location.Hand])
                hand.AddChild(card.GetView().transform);
            yield return ScaleCoroutine.Play(handShuffleDuration, hand.transform, initialScale);
        }

        private IEnumerator ShuffleStack(Player player, Location location)
        {
            if (player[location].Count < 3)
                yield break;

            PlaySfx(deckShuffle);
            var transform = player.GetComponent<PlayerView>().GetLocation(location).transform;

            for (int i = 0; i < shuffleCount; i++)
            {
                var view = transform.GetChild(transform.childCount -1);
                var position = view.position;
                yield return MoveCoroutine.Play(deckShuffleDuration, view, position + offset);
                yield return MoveCoroutine.Play(deckShuffleDuration, view, position);
            }
        }
    }
}
