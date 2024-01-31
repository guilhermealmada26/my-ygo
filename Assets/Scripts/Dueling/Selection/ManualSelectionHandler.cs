using BBG.Dueling.Actions;
using System.Collections;
using TMPro;
using UnityEngine;

namespace BBG.Dueling.Selection
{
    public abstract class ManualSelectionHandler<T> : DuelComponent where T : SelectionAction
    {
        [SerializeField] GameObject panel;
        [SerializeField] TextMeshProUGUI txMessage;

        protected T selection;
        private RemoteSelectionHandler remoteSelectionHandler;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<T>(OnSelectionPerform);
            remoteSelectionHandler = FindObjectOfType<RemoteSelectionHandler>();
        }

        private void OnSelectionPerform(DuelAction action)
        {
            selection = action as T;
            if (selection.player.Control != ControlMode.Manual || !IsValid(selection))
                return;

            Execute(WaitUntilSelected());
        }

        protected virtual bool IsValid(T selection) => true;

        private IEnumerator WaitUntilSelected()
        {
            SelectionStarted(selection);
            yield return new WaitUntil(() => selection.IsFinished());
            SelectionEnded(selection);
            remoteSelectionHandler.SendSelectionData(selection);
            yield return new WaitForSeconds(.3f);
        }

        protected virtual void SelectionStarted(T selection)
        {
            panel.SetActive(true);
            txMessage.text = selection.message;
        }

        protected virtual void SelectionEnded(T selection)
        {
            panel.SetActive(false);
        }

        public virtual void Confirm() => selection.Confirm();

        public void Cancel() => selection.Cancel();
    }
}