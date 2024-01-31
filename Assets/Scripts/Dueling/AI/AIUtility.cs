using System.Collections.Generic;

namespace BBG.Dueling.AI
{
    static class AIUtility
    {
        public static List<Monster> GetMonsters(this Player player, Location location)
        {
            var monsters = new List<Monster>(5);
            foreach(var card in player[location])
                if(card is Monster monster)
                    monsters.Add(monster);
            return monsters;
        }

        public static List<Card> GetActivatable(this Player player, params Location[] locations)
        {
            var cards = new List<Card>();
            locations.Foreach(l => AddCards(cards, l, player));
            return cards;
        }

        private static void AddCards(List<Card> cards, Location location, Player player)
        {
            foreach(var card in player[location])
                if(card.effects.Count > 0)
                    cards.Add(card);
        }
    }
}