using System.Collections.Generic;

namespace BBG.Dueling
{
    public interface ICardsHolder
    {
        public IEnumerable<Card> GetCards();
    }
}