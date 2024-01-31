using UnityEngine;
using UnityEngine.EventSystems;

namespace BBG.Ygo.DeckEditing
{
    public class CardMovement : CardHolder, IPointerClickHandler
    {
        [SerializeField] bool inDeck;
        [SerializeField] AudioClip invalidSfx;

        private DeckEditor editor;
        private SoundManager soundManager;

        private void Awake()
        {
            soundManager = SoundManager.Instance;
        }

        public void SetEditor(DeckEditor editor)
        {
            this.editor = editor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
                return;

            if (inDeck)
            {
                editor.RemoveCardFromDeck(card);
            }
            else
            {
                var addedToDeck = editor.AddCardToDeck(card);
                if (!addedToDeck)
                    soundManager.PlaySfx(invalidSfx);
            }
        }
    }
}
