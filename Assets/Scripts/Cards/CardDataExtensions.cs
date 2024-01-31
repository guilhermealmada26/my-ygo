using System.Collections.Generic;
using System.Linq;

namespace BBG.Ygo
{
    static class CardDataExtensions
    {
        public static string[] GetIDs(this IEnumerable<CardData> cards)
        {
            var ids = new string[cards.Count()];
            int i = 0;

            foreach(var card in cards)
            {
                ids[i] = card.ID;
                i++;
            }

            return ids;
        }
    }
}