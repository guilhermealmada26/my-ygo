using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class DuelCardViewSetup : DuelComponent
    {
        [SerializeField] DuelCardView prefab;

        private static Dictionary<string, DuelCardView> cardViews;

        internal static DuelCardView Get(string duelId) => cardViews[duelId];

        private void Awake()
        {
            cardViews = new();
        }

        private void OnDestroy()
        {
            cardViews = null;
        }

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            foreach(var card in duel.Cards)
            {
                var cardView = Instantiate(prefab);
                cardView.SetCard(card.Value);
                cardView.UpdateLocation();
                cardViews.Add(card.Key, cardView);
            }
        }        
    }
}
