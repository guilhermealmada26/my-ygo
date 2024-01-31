using BBG.Dueling.Settings;
using BBG.Ygo;

namespace BBG.Dueling
{
    public class CardFactory
    {
        public static void CreateCards(PlayerData data, Player player)
        {
            for (int i = data.mainDeck.Length - 1; i >= 0; i--)
                Create(CardDatabase.Get(data.mainDeck[i]), player, i);

            for (int i = data.extraDeck.Length - 1; i >= 0; i--)
                Create(CardDatabase.Get(data.extraDeck[i]), player, i);
        }

        static void Create(CardData data, Player player, int index)
        {
            if (data == null)
                return;
            var cardId = $"{(data.GoesToExtraDeck ? 'E' : 'D')}{player.ID}-{index}";
            Card card;

            if (data is MonsterData monsterData)
                card = new Monster(monsterData, player, cardId);
            else
                card = new Card(data, player, cardId);

            Duel.Current.Cards.Add(card.id, card);
        }
    }
}