using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class DeclarableActionsView : DuelComponent
    {
        [SerializeField] DeclarableActionsSystem system;

        private HashSet<GameObject> toDisable = new();

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            system.ActionsAvailable += OnActionsAvailable;
            system.ActionsCleared += OnActionsCleared;
        }

        private void OnActionsAvailable()
        {
            if (duel.CurrentPlayer.Control != ControlMode.Manual)
                return;

            var actions = system.AvailableActions;
            var inHandAndField = actions.Where(a => a.Key.Location.IsHandOrField());
            //enable has actions highlight in each card that has actions
            foreach (var pair in inHandAndField)
            {
                var hasActions = pair.Key.GetView().hasActions;
                hasActions.SetActive(true);
                toDisable.Add(hasActions);
            }

            //if graveyard/extra deck/ banished has any card with actions, show zone highlight
            var playerView = duel.CurrentPlayer.GetComponent<PlayerView>();
            if (actions.Any(a => a.Key.Location == Location.Graveyard))
            {
                playerView.highlightGraveyard.SetActive(true);
                toDisable.Add(playerView.highlightGraveyard);
            }
            if (actions.Any(a => a.Key.Location == Location.Banished))
            {
                playerView.highlightBanished.SetActive(true);
                toDisable.Add(playerView.highlightBanished);
            }
            if (actions.Any(a => a.Key.Location == Location.ExtraDeck))
            {
                playerView.highlightExtraDeck.SetActive(true);
                toDisable.Add(playerView.highlightExtraDeck);
            }
        }

        private void OnActionsCleared()
        {
            toDisable.Foreach(o => o.SetActive(false));
            toDisable.Clear();
        }
    }
}
