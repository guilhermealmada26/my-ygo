using BBG.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo
{
    [CreateAssetMenu(menuName = "Cards/CardsCollection")]
    public class CardCollection : CollectionSO<CardData>
    {
        public int CardCount => Assets.Length;

        [ContextMenu("Sort Cards")]
        public void Sort()
        {
            List<CardData> cards = new(assets);
            cards.Sort(new CardComparer());
            assets = cards.ToArray();
        }

        [ContextMenu("PRINT NULL")]
        public void PrintCardsWithNullEffects()
        {
            foreach (var card in assets)
            {
                foreach (var eff in card.effects)
                {
                    if (eff == null)
                    {
                        Debug.Log($"Card {card.name} has a null effect.");
                        break;
                    }
                }
            }
        }
    }
}