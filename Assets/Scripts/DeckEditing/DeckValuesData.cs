namespace BBG.Ygo.DeckEditing
{
    public class DeckValuesData
    {
        public int equippedIndex;
        public string[] malletIds;
        public DeckSerializedData[] decks;

        public DeckValuesData(DeckValues values)
        {
            equippedIndex = values.EquippedIndex;
            malletIds = values.mallet.GetIDs();
            decks = new DeckSerializedData[values.decks.Count];
            for (int i = 0; i < decks.Length; i++)
                decks[i] = new DeckSerializedData(values.decks[i]);
        }

        public DeckValues GetValues()
        {
            var values = new DeckValues
            {
                mallet = CardDatabase.GetCards(malletIds),
                decks = new System.Collections.Generic.List<Deck>(decks.Length)
            };

            decks.Foreach(d => values.decks.Add(d.GetNewDeck()));
            values.equipped = values.decks[equippedIndex];
            return values;
        }
    }
}
