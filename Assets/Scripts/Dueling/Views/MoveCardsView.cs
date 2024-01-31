using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class MoveCardsView : DuelComponent
    {
        [SerializeField] EventName actionName;
        [SerializeField] float duration;
        [SerializeField] AudioClip clip;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe(actionName, (a) => Execute(OnActionCardsMoved(a)));
        }

        private IEnumerator OnActionCardsMoved(DuelAction a)
        {
            var action = a as ICardsHolder;
            foreach (var card in action.GetCards())
                yield return CardViewMovement.MoveCard(card.GetView(), duration, clip);
        }
    }
}
