using UnityEngine;

namespace BBG.Dueling.Effects
{
    public class OnMovedResolve : OnLocationFeature
    {
        protected override void OnApply()
        {
            if (effect.CanResolve())
                effect.Resolve();
            if (effect.card.data.cardName == "Premature Burial")
                Debug.Log(effect.card.data.cardName + effect.card.equippedTo);

        }

        protected override void OnUnApply()
        {
        }
    }
}