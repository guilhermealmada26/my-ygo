using BBG.Dueling.Actions;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling
{
    public class TurnActionsRegistry : DuelComponent
    {
        readonly HashSet<DuelAction> performedThisTurn = new();

        internal static TurnActionsRegistry Instance { private set; get; }

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<ChangeTurnAction>(OnChangeTurn);
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void OnChangeTurn(DuelAction action)
        {
            performedThisTurn.Clear();
        }

        internal void Add(DuelAction action)
        {
            performedThisTurn.Add(action);
        }

        internal List<T> PerformedThisTurn<T>() where T : DuelAction
        {
            return performedThisTurn.OfType<T>().ToList();
        }

        internal List<DuelAction> PerformedThisTurn(EventName eventName)
        {
            return performedThisTurn.Where(a => a.GetType().Name == eventName.ToString()).ToList();
        }

        internal List<T> PerformedThisTurn<T>(Card card) where T : CardAction
        {
            return performedThisTurn.OfType<T>().Where(a => a.card == card).ToList();
        }
    }
}