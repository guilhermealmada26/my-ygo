using BBG.Animations;
using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class SpecialSummonView : DuelComponent
    {
        [SerializeField] ParticleAnimation defaultAnimation, ancientAnimation, ritualAnimation;
        [SerializeField] float moveDuration;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<SpecialSummon>((a) => Execute(OnSummon(a as SpecialSummon)));
        }

        private IEnumerator OnSummon(SpecialSummon action)
        {
            var view = action.Monster.GetView();
            yield return view.MoveCard(moveDuration);
            var anim = GetAnimation(action.Monster, action);
            yield return anim.Play(view.transform.position, null);
            view.UpdateStats();
        }

        private ParticleAnimation GetAnimation(Card card, SpecialSummon action)
        {
            if (action.reason != Reason.ExtraSummon)
                return defaultAnimation;
            var type = card.data.type.subtype;
            if (type == Ygo.Subtype.Ancient)
                return ancientAnimation;
            if(type == Ygo.Subtype.Ritual)
                return ritualAnimation;
            return defaultAnimation;
        }
    }
}
