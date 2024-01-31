using BBG.Dueling.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling
{
    public class DeclarableActionsSystem : DuelComponent
    {
        [SerializeField] Location[] locations;

        readonly Dictionary<Card, List<DuelAction>> available = new();
        readonly DeclarableActionsFactory factory = new();

        public event System.Action ActionsAvailable, ActionsCleared;
        public Dictionary<Card, List<DuelAction>> AvailableActions => available;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            ActionProcessor.Instance.OnSequenceEnd += OnSequenceEnd;
            ActionProcessor.Instance.OnSequenceStart += OnSequenceStart;
        }

        private void OnSequenceEnd()
        {
            if (duel.CurrentPlayer.Control == ControlMode.Manual)
            {
                foreach (var location in locations)
                {
                    foreach (var card in duel.CurrentPlayer[location])
                    {
                        var actions = factory.Get(card);
                        if (actions.Count > 0)
                            available.Add(card, actions);
                    }
                }
            }
            ActionsAvailable?.Invoke();
        }

        private void OnSequenceStart()
        {
            if (duel.CurrentPlayer.Control != ControlMode.Manual)
                return;
            foreach (var pair in available)
                pair.Value.Clear();
            available.Clear();
            ActionsCleared?.Invoke();
        }
    }
}