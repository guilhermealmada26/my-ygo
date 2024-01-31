using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/BanishCards")]
    public class BanishCardsEffect : CardsActionEffect
    {
        protected override DuelAction GetAction(Card target)
        {
            return new MoveCardAction(target, Location.Banished, card);
        }
    }
}