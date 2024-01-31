using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using BBG.Dueling.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Replay
{
    public class ActionRecorder : DuelComponent
    {
        [SerializeField] bool saveDuelAsLastReplay;
        [SerializeField] List<string> declaredActions;
        [SerializeField] List<string> selectionDatas;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            ActionDeclarer.Instance.ActionDeclared += OnActionDeclared;
            Events.Observe<SelectionAction>(OnSelectionPerform);
        }

        private void OnActionDeclared(DuelAction action)
        {
            declaredActions.Add(ActionSerializer.Serialize(action));
        }

        private void OnSelectionPerform(DuelAction action)
        {
            var selection = action as SelectionAction;
            selection.AfterSelection += OnSelectionConfirmed;
        }

        private void OnSelectionConfirmed(SelectionAction selection)
        {
            selectionDatas.Add(selection.GetLoadableData());
            selection.AfterSelection -= OnSelectionConfirmed;
        }

        private ReplayData GetDuelRegistry()
        {
            return new ReplayData(DuelInitializer.Configuration, declaredActions.ToArray(), selectionDatas.ToArray());
        }

        [ContextMenu("SAVE REPLAY")]
        internal void SaveLastDuel()
        {
            if (!saveDuelAsLastReplay || duel.LocalPlayer.Control == ControlMode.Replay)
                return;

            var json = JsonUtility.ToJson(GetDuelRegistry(), false);
            var path = Application.streamingAssetsPath + "/Replays/LAST.rpy";
            System.IO.File.WriteAllText(path, json);
        }
    }
}