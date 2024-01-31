using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.AI
{
    public class AIHandler : DuelComponent
    {
        [SerializeField] AIContainer container;
        private SelectionAction selection;

        public void SetAiContainer(AIContainer container)
        {
            this.container = container;
        }

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<SelectionAction>(OnSelectionPerform);
            FindObjectOfType<DeclarableActionsSystem>().ActionsAvailable += OnGameIdle;
        }

        private void OnGameIdle()
        {
            if (duel.CurrentPlayer.Control != ControlMode.AI)
                return;
            StartCoroutine(DeclareAction());
        }

        private IEnumerator DeclareAction()
        {
            yield return new CoroutineQueue.WaitCoroutinesEnd();
            container.DeclareAction(duel.CurrentPlayer);
        }

        private void OnSelectionPerform(DuelAction action)
        {
            selection = (SelectionAction)action;
            if (selection.player.Control != ControlMode.AI)
                return;

            container.HandleSelection(selection);
            new DelegateAction(Confirm).Perform(Priority.AboveAll);
        }

        private void Confirm() => selection.Confirm();
    }
}