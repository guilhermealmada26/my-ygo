using BBG.Animations;
using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class PositionChangeView : DuelComponent
    {
        [SerializeField] AudioClip clip;
        [SerializeField] float duration = .5f;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ChangePositionAction>((a) => Execute(OnPositionChanged(a)));
            Events.Observe<FlipPositionAction>((a) => Execute(OnPositionChanged(a)));
        }

        private IEnumerator OnPositionChanged(DuelAction a)
        {
            var action = a as ICardsHolder;

            foreach(var card in action.GetCards())
            {
                var view = card.GetView();
                if (a is FlipPositionAction)
                {
                    PlaySfx(clip);
                    yield return RotateCoroutine.RotateLocal(duration, view.transform, view.PositionRotation);
                    view.UpdateStats();
                }
                else
                {
                    if (card.Location == Location.MonsterZone)
                        PlaySfx(clip);
                    view.UpdateStats();
                    yield return new WaitForSeconds(.3f);
                }
            }
        }
    }
}
