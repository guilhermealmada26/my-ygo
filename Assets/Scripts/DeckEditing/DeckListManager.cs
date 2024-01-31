using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    public class DeckListManager : MonoBehaviour
    {
        [SerializeField] SceneLoader sceneLoader;
        [SerializeField] string editDeckSceneName;
        [SerializeField] bool showDeckDetails, readOnly;
        [SerializeField] DeckSlot firstSlot;
        [SerializeField] int maxDecks = 50;
        [SerializeField] GameObject detailsPanel, buttonsPanel, deckRenamePanel;
        [SerializeField] TMP_InputField deckNameIF;

        private DeckSlot selected;
        private DeckSlot[] slots;
        private List<Deck> decks;
        private DeckValues values;

        private void Awake()
        {
            slots = new DeckSlot[maxDecks];
            slots[0] = firstSlot;
            for (int i = 1; i < maxDecks; i++)
                slots[i] = Instantiate(firstSlot, firstSlot.transform.parent);

            detailsPanel.SetActive(showDeckDetails);
            buttonsPanel.SetActive(!readOnly);
        }

        private void Start()
        {
            values = DeckValuesManager.Current;
            Setup();
        }

        public void Setup()
        {
            decks = values.decks;
            decks.Filter(d => d != null);
            UpdateUI();
            Clicked(slots[values.EquippedIndex]);
        }

        private void UpdateUI()
        {
            for (int i = 0; i < decks.Count; i++)
            {
                slots[i].SetDeck(decks[i], values.mallet);
                slots[i].gameObject.SetActive(true);
                slots[i].SetEquipped(false);
            }
            for (int i = decks.Count; i < maxDecks; i++)
            {
                slots[i].gameObject.SetActive(false);
            }
            slots[values.EquippedIndex].SetEquipped(true);
        }

        public void Clicked(DeckSlot deckSlot)
        {
            if (deckSlot == selected)
                return;

            if (selected != null)
                selected.OnDeselect();
            deckSlot.OnSelect();
            selected = deckSlot;
        }

        public void NewDeck()
        {
            var newDeck = ScriptableObject.CreateInstance<Deck>();
            NewName("#", newDeck);
            decks.Add(newDeck);
            DeckValuesManager.SetDeckToEdit(newDeck);
            sceneLoader.LoadScene(editDeckSceneName);
        }

        void NewName(string prefix, Deck deck)
        {
            deck.name = string.Empty;
            do
            {
                var name = prefix + Random.Range(0, 2000);
                DeckValuesManager.TryRename(deck, name);
            }
            while (deck.name == string.Empty);
        }

        public void EditCurrent()
        {
            if (selected == null)
                return;

            DeckValuesManager.SetDeckToEdit(selected.Deck);
            sceneLoader.LoadScene(editDeckSceneName);
        }

        private bool SelectedIsEquipped => selected == slots[values.EquippedIndex];

        public void EquipCurrent()
        {
            if (selected == null || SelectedIsEquipped || !selected.Deck.IsValid(values.mallet))
                return;

            slots[values.EquippedIndex].SetEquipped(false);
            values.equipped = selected.Deck;
            UpdateUI();
        }

        public void ShowRenameMenu()
        {
            if (selected == null)
                return;

            deckRenamePanel.SetActive(true);
            deckNameIF.text = selected.Deck.name;
        }

        public void RenameCurrent()
        {
            if (selected == null)
                return;

            DeckValuesManager.TryRename(selected.Deck, deckNameIF.text);
            deckRenamePanel.SetActive(false);
            UpdateUI();
        }

        public void CloneCurrent()
        {
            if (selected == null)
                return;

            var clone = selected.Deck.Clone();
            NewName("Clone:", clone);
            decks.Add(clone);
            UpdateUI();
        }

        public void DeleteCurrent()
        {
            if (SelectedIsEquipped || selected == null)
                return;

            selected.OnDeselect();
            decks.Remove(selected.Deck);
            selected = null;
            UpdateUI();
        }
    }
}