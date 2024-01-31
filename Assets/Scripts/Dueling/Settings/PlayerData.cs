using BBG.Ygo;
using BBG.Ygo.DeckEditing;

namespace BBG.Dueling.Settings
{
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public string imageName;
        public ControlMode control;
        public int initialLP = 8000;
        public int initialDrawCount = 5;
        public string[] mainDeck;
        public string[] extraDeck;

        public void SetData(Player player)
        {
            player.Data = this;
            player.LP = initialLP;
            CardFactory.CreateCards(this, player);
        }

        public void SetDeck(Deck deck)
        {
            mainDeck = deck.MainDeck.GetIDs();
            extraDeck = deck.ExtraDeck.GetIDs();
        }
    }
}