using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    public abstract class CardsGetter : ScriptableObject
    {
        public abstract List<Card> GetCards(Effect caller);

        public virtual List<Card> GetCards(Effect caller, Card toBeChecked)
        {
            return GetCards(caller);
        }
    }
}