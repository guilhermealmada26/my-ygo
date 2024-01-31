using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class ModifyCardsView : DuelComponent
    {
        [SerializeField] EventName actionName;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe(actionName, CardsModified);
        }

        private void CardsModified(DuelAction action)
        {
            (action as CardAction).card.GetView().UpdateStats();
        }
    }
}
