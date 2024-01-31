using BBG.Animations;
using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class SummonView : DuelComponent
    {
        [SerializeField] EventName summonName;
        [SerializeField] float moveDuration;
        [SerializeField] ParticleAnimation summonAnimation;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe(summonName, (a) => Execute(OnSummon(a)));
        }

        private IEnumerator OnSummon(DuelAction action)
        {
            var summon = action as SummonAction;
            var view = summon.Monster.GetView();
            if (moveDuration > 0)
                yield return view.MoveCard(moveDuration);
            else
                view.UpdateStats();
            if(summonAnimation != null)
                yield return summonAnimation.Play(view.transform.position, null);
        }
    }
}
