using BBG.Animations;
using BBG.Dueling.Actions;
using BBG.Ygo;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class MaterialDiscardView : DuelComponent
    {
        [SerializeField] ParticleAnimation ancientSummon, fusionSummon;
        [SerializeField] float scaleDuration = .3f;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<MaterialDiscard>((a) => Execute(OnMaterialDiscard(a as MaterialDiscard)));
        }

        private IEnumerator OnMaterialDiscard(MaterialDiscard action)
        {
            var card = action.card;
            var view = card.GetView();
            var isAncient = action.summoned.data.type.subtype == Subtype.Ancient;
            var anim = isAncient ? ancientSummon : fusionSummon;
            StartCoroutine(ScaleOut(view));
            var cardPos = view.transform.position;
            var destination = view.ControllerView.GetLocation(card.Location);
            StartCoroutine(anim.Play(cardPos, isAncient ? null : destination.transform.position));
            yield return new WaitForSeconds(anim.Duration);
        }

        private IEnumerator ScaleOut(DuelCardView view)
        {
            yield return ScaleCoroutine.Play(scaleDuration, view.transform, Vector3.zero);
            view.UpdateLocation();
        }
    }
}
