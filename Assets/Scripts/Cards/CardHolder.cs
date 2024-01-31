using UnityEngine;

namespace BBG.Ygo
{
    public class CardHolder : MonoBehaviour
    {
        public CardData card { private set; get; }

        public void SetData(CardData data)
        {
            var holders = GetComponents<CardHolder>();

            foreach (var holder in holders)
            {
                holder.card = data;
                holder.OnDataChanged();
            }
        }

        protected virtual void OnDataChanged() { }

        public void UpdateStats()
        {
            var holders = GetComponents<CardHolder>();

            foreach (var holder in holders)
            {
                holder.OnUpdateStats();
            }
        }

        protected virtual void OnUpdateStats() { }

    }
}