using BBG.Dueling.Actions;
using BBG.Dueling.PlayersGetters;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/Search")]
    public class SearchEffect : CardsEffect
    {
        public PlayersGetter shuffleTarget;

        internal override void Resolve(List<Card> cards)
        {
            foreach (var target in cards)
                new MoveCardAction(target, Location.Hand, card).Perform();
            foreach (var ply in shuffleTarget.GetPlayers(this))
                new ShuffleAction(ply, Location.Deck).Perform();
        }
    }
}