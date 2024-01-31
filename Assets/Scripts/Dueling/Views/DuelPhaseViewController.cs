using BBG.Dueling.Actions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBG.Dueling.View
{
    public class DuelPhaseViewController : DuelComponent
    {
        [SerializeField] TextMeshProUGUI turnCount, currentPhase, popupMessage;
        [SerializeField] Button battlePhase, endTurn;
        [SerializeField] Animator popupAnimator;
        [SerializeField] AudioClip changePhase, changeTurn;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ChangePhaseAction>(PhaseChanged);
            Events.Observe<ChangeTurnAction>(TurnChanged);
            TurnChanged(null);
            PhaseChanged(null);
            battlePhase.onClick.AddListener(() => ChangePhase(DuelPhase.Battle));
            endTurn.onClick.AddListener(() => ChangePhase(DuelPhase.End));
            battlePhase.interactable = false;
            endTurn.interactable = currentPlayer.Control == ControlMode.Manual;
            ActionProcessor.Instance.OnSequenceStart += DisableButtons;
            ActionProcessor.Instance.OnSequenceEnd += SetButtonsState;
        }

        private void PhaseChanged(DuelAction action)
        {
            currentPhase.text = duel.Phase + " Phase";
            if (action == null)
                return;

            if (duel.Phase == DuelPhase.Battle)
            {
                Execute(MessagePopup(currentPhase.text));
                PlaySfx(changePhase);
            }
            else
            {
                Wait(0.17f);
            }
        }

        private IEnumerator MessagePopup(string message)
        {
            popupMessage.text = message;
            popupAnimator.SetTrigger("Popup");
            yield return new WaitWhile(() => popupAnimator.IsAnimating());
        }

        private void TurnChanged(DuelAction action)
        {
            turnCount.text = duel.TurnCount.ToString();
            currentPlayer.GetComponent<PlayerView>().Highlight(true);
            currentPlayer.Opponent.GetComponent<PlayerView>().Highlight(false);
            if (action == null)
                return;

            Execute(MessagePopup(currentPlayer.Data.name + "'s turn"));
            PlaySfx(changeTurn);
        }

        private void SetButtonsState()
        {
            var isManual = currentPlayer.Control == ControlMode.Manual;
            battlePhase.interactable = Action(DuelPhase.Battle).CanPerform() && isManual;
            endTurn.interactable = Action(DuelPhase.End).CanPerform() && isManual;
        }

        private void DisableButtons()
        {
            battlePhase.interactable = endTurn.interactable = false;
        }

        private ChangePhaseAction Action(DuelPhase phase) => new (phase);

        private void ChangePhase(DuelPhase phase) => Action(phase).Declare();
    }
}
