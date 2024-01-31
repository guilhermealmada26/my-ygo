using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Replay
{
    public class ReplayManager : DuelComponent
    {
        [SerializeField] GameObject replayMenu;
        [SerializeField] ToggleImageColor playBT, fastBT;

        [SerializeField] List<string> actions, selections;

        public void SetData(ReplayData replayData)
        {
            actions = new List<string>(replayData.declaredActions);
            selections = new List<string>(replayData.selectionDatas);
        }

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            if (duel.LocalPlayer.Control != ControlMode.Replay)
                return;

            FindObjectOfType<DeclarableActionsSystem>().ActionsAvailable += OnGameIdle;
            Events.Observe<SelectionAction>(OnSelectionPerform);
            replayMenu.SetActive(true);
            SetButtonsColor();
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
            AudioListener.pause = false;
        }

        private void OnGameIdle()
        {
            StartCoroutine(Next());
        }

        IEnumerator Next()
        {
            yield return new WaitWhile(() => CoroutineQueue.Instance.Executing);
            if (actions.Count > 0) 
                NextAction(); 
            else 
                EndDuel();
        }

        private void NextAction()
        {
            var action = ActionSerializer.Deserialize(actions[0]);
            actions.RemoveAt(0);
            if (action.CanPerform())
                action.Perform(Priority.Manual);
            else
                NextAction();
        }

        private void EndDuel()
        {
            new EndDuelAction(EndDuelAction.Reason.ReplayEnded).Perform(Priority.AboveAll);
        }

        private void OnSelectionPerform(DuelAction action)
        {
            var selection = action as SelectionAction;
            selection.Confirm(selections[0]);
            selections.RemoveAt(0);
        }

        private void SetButtonsColor()
        {
            //true = white, false = gray
            playBT.Set(Time.timeScale > 0);
            fastBT.Set(Time.timeScale > 2);
        }

        public void Pause()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            AudioListener.pause = Time.timeScale == 0;
            SetButtonsColor();
        }

        public void FastForward()
        {
            if (Time.timeScale == 0)
                return;
            Time.timeScale = Time.timeScale == 1 ? 2.2f : 1;
            SetButtonsColor();
        }
    }
}