using BBG.Animations;
using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class ParticleMoveCardsView : DuelComponent
    {
        [SerializeField] EventName actionName;
        [SerializeField] ParticleAnimation particleAnimation;
        [SerializeField] bool moveParticle;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe(actionName, (a) => Execute(OnActionCardsMoved(a)));
        }

        private IEnumerator OnActionCardsMoved(DuelAction action)
        {
            var moveAction = action as ICardsHolder;
            foreach(var card in moveAction.GetCards())
            {
                var view = card.GetView();
                var cardPos = view.transform.position;
                view.UpdateLocation();
                view.gameObject.SetActive(false);
                if (moveParticle)
                {
                    var destination = view.ControllerView.GetLocation(card.Location);
                    yield return particleAnimation.Play(cardPos, destination.transform.position);
                }
                else
                {
                    yield return particleAnimation.Play(cardPos);
                }
                view.gameObject.SetActive(true);
            }
        }
    }
}
