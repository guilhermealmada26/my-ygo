using UnityEngine;

namespace BBG.Ygo.DeckEditing
{
    public class CustomDeckEdit : MonoBehaviour
    {
        [SerializeField] Deck deckToEdit;

        void Start() { }

        private void Awake()
        {
            if (!enabled || deckToEdit == null)
                return;

            var editor = GetComponent<DeckEditorController>();
            editor.EditingDeck = deckToEdit;
            var mallet = new System.Collections.Generic.List<CardData>();
            for (int i = 0; i < 3; i++)
                CardDatabase.GetAllCards().Foreach(c => mallet.Add(c));
            editor.Mallet = mallet;
            Debug.LogWarning("CUSTOM DECK EDIT MODE");
        }
    }
}
