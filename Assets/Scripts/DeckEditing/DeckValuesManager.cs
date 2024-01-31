using BBG.Saving;
using System.Linq;
using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    public class DeckValuesManager : SaveDataHolder<DeckValuesData>
    {
        [SerializeField] DeckValues values;
        AlertMessage alertMessage;

        static DeckValuesManager Instance { set; get; }
        static string deckToEditName;

        public static DeckValues Current => Instance.values;

        public static Deck GetDeckToEdit()
        {
            return Instance.values.decks.Find(d => d.name == deckToEditName);
        }

        public static void SetDeckToEdit(Deck value)
        {
            deckToEditName = value.name;
        }

        protected override void OnLoad(DeckValuesData data)
        {
            Instance = this;
            alertMessage = FindObjectOfType<AlertMessage>();
            values = data.GetValues();
        }

        protected override void BeforeSave()
        {
            SavedData = new DeckValuesData(values);
        }

        protected override DeckValuesData GetDefaultData() => new(values);

        [ContextMenu("ADD DECK CARDS")]
        void AddDeckCardsToMallet()
        {
            values.AddDeckCardsToMallet();
        }

        public static bool HasDeckName(string name) => Instance.values.decks.Any(d => d.name == name);

        public static void TryRename(Deck deck, string newName)
        {
            if (deck.name == newName)
                return;
            if (HasDeckName(newName))
            {
                Instance.alertMessage.ShowMessage("There is another deck with this name already");
                return;
            }
            deck.name = newName;
        }
    }
}
