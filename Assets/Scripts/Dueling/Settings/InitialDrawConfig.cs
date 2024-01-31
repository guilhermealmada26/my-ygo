using BBG.Dueling.Actions;
using BBG.Dueling.Settings;
using UnityEngine;

namespace BBG.Dueling.Network
{
    [CreateAssetMenu(menuName = "Duel/Settings/InitialDrawConfig")]
    public class InitialDrawConfig : ConfigurationExtender
    {
        public bool shuffleDecks;

        internal override void Setup(Duel duel)
        {
            var p1 = duel.GetPlayer(0);
            var p2 = duel.GetPlayer(1);
            var actions = duel.StartingActions;

            if (shuffleDecks)
            {
                actions.Add(new ShuffleAction(p1, Location.Deck, false));
                actions.Add(new ShuffleAction(p2, Location.Deck, false));
            }

            actions.Add(new DrawCardsAction(p1, p1.Data.initialDrawCount, Reason.Other));
            actions.Add(new DrawCardsAction(p2, p2.Data.initialDrawCount, Reason.Other));
        }
    }
}