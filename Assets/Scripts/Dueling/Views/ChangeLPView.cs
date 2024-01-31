using BBG.Animations;
using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class ChangeLPView : DuelComponent
    {
        [SerializeField] float animationDuration = 0.8f;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ChangeLPAction>((a) => Execute(OnChangeLP(a)));
        }

        [ContextMenu("CHANGE LP")]
        private void ChangeLp()
        {
            new ChangeLPAction(currentPlayer, 3000, Reason.Other, null).Declare();
        }

        private IEnumerator OnChangeLP(DuelAction action)
        {
            var changeLP = action as ChangeLPAction;
            if (changeLP.amount == 0)
                yield break;
            var player = changeLP.player;
            var difference = player.LP - changeLP.PreviousLP;

            var portrait = player.GetComponent<PlayerView>().Portrait;
            if (difference > 0)
            {
                portrait.TriggerHeal();
                portrait.SetDamageText("+" + difference);
            }
            else
            {
                portrait.TriggerDamage();
                portrait.SetDamageText(difference.ToString());
            }

            yield return new WaitForSeconds(.4f);
            var lpAnimation = new ChangeValueAnimation(portrait.LpText);
            var finalLp = Mathf.Clamp(player.LP, 0, int.MaxValue);
            yield return lpAnimation.ChangeValueOverTime(finalLp, changeLP.PreviousLP, animationDuration);
        }
    }
}
