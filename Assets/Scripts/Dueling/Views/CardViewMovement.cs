using BBG.Animations;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public static class CardViewMovement
    {
        public static void UpdateLocation(this DuelCardView view)
        {
            SetAtLocation(view, view.card.Location);
        }

        public static void SetAtLocation(this DuelCardView view, Location location)
        {
            var playerView = view.ControllerView;
            var parent = playerView.GetLocation(location);
            if (parent == null)
                return;

            parent.AddChild(view.transform);
            view.UpdateStats();
        }

        public static IEnumerator MoveCard(this DuelCardView view, Location destination, float duration, AudioClip clip = null)
        {
            var pView = view.ControllerView;
            var location = pView.GetLocation(destination);
            if (location.transform == view.transform.parent)
                yield break;
            view.transform.localScale = Vector3.one * .2f;
            yield return new WaitForSeconds(duration / 5);
            SoundManager.Instance.PlaySfx(clip);
            yield return MoveCoroutine.Play(duration, view.transform, location.transform.position);
            location.AddChild(view.transform);
            view.UpdateStats();
            yield return new WaitForEndOfFrame();
            location.AdjustChildren();
        }

        public static IEnumerator MoveCard(this DuelCardView view, float duration, AudioClip clip = null)
        {
            yield return MoveCard(view, view.card.Location, duration, clip);
        }
    }
}
