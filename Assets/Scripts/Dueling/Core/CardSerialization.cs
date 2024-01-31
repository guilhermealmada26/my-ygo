using System.Collections.Generic;

namespace BBG.Dueling
{
    public static class CardSerialization
    {
        public static string GetIds(List<Card> cards)
        {
            var str = "";
            foreach (var card in cards)
                str += $"{card.id};";
            return str;
        }

        public static List<Card> GetCards(string data)
        {
            var cards = Duel.Current.Cards;
            var ids = data.Split(';');
            var list = new List<Card>();
            foreach (var id in ids)
                if(cards.ContainsKey(id))
                    list.Add(cards[id]);
            return list;
        }
    }
}