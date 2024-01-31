using System.Collections.Generic;
using UnityEngine;

namespace BBG.Ygo
{
    public class CardDatabase : MonoBehaviour
    {
        private static CardDatabase instance;
        private static Dictionary<string, CardData> cache = new();

        private void Awake()
        {
            instance = this;
        }

        public CustomCardStats customStats;

        public static string GetCustomAtk(CardData card)
        {
            if (instance.customStats == null || !instance.customStats.customAtks.ContainsKey(card))
                return "";
            return instance.customStats.customAtks[card];
        }

        public static string GetCustomDef(CardData card)
        {
            if (instance.customStats == null || !instance.customStats.customDefs.ContainsKey(card))
                return "";
            return instance.customStats.customDefs[card];
        }

        public static CardData Get(string id)
        {
            if (!cache.ContainsKey(id))
                cache.Add(id, Resources.Load<CardData>("Cards/" + id));
            return cache[id];
        }

        public static List<CardData> GetCards(string[] ids)
        {
            var cards = new List<CardData>();
            foreach (string id in ids)
                cards.Add(Get(id));
            return cards;
        }

        public static CardData[] GetAllCards()
        {
            return Resources.LoadAll<CardData>("Cards");
        }
    }
}