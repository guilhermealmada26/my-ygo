using BBG.Dueling.Actions;
using BBG.Ygo.CardViews;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class EffectsView : DuelComponent
    {
        [SerializeField] AudioClip activateSfx, negateSfx;
        [SerializeField] CardView popupCard;

        Animator _animator;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            _animator = popupCard.GetComponent<Animator>();
            Events.Observe<ActivationAction>((a) => Execute(OnActivate(a)));
            Events.Observe<ResolveAction>((a) => Execute(OnResolve(a)));
            Events.Observe<ShowResolveAction>((a) => Execute(OnShowResolve(a)));
            Events.Observe<NegateAction>((a) => Execute(OnNegate(a)));
        }

        private IEnumerator OnActivate(DuelAction a)
        {
            var action = a as ActivationAction;
            var view = action.card.GetView();
            view.SetLinkView(action.ChainLink);
            PlaySfx(activateSfx);
            popupCard.SetData(action.card.data);
            _animator.SetTrigger("activate");
            yield return new WaitForSeconds(.9f);
        }

        private IEnumerator OnResolve(DuelAction a)
        {
            var action = a as CardAction;
            var view = action.card.GetView();
            view.DisableLinkView();
            yield return new WaitForSeconds(.3f);
        }

        private IEnumerator OnShowResolve(DuelAction a)
        {
            PlaySfx(activateSfx);
            var action = a as CardAction;
            popupCard.SetData(action.card.data);
            _animator.SetTrigger("activate");
            yield return new WaitForSeconds(.7f);
        }

        private IEnumerator OnNegate(DuelAction a)
        {
            var action = a as NegateAction;
            var view = action.card.GetView();
            view.DisableLinkView();
            PlaySfx(negateSfx);
            popupCard.SetData(action.card.data);
            _animator.SetTrigger("negate");
            yield return new WaitForSeconds(.8f);
        }
    }
}
