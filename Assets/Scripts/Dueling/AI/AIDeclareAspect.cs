using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.AI
{
    public abstract class AIDeclareAspect : ScriptableObject
    {
        public abstract DuelAction GetAction(Player player);

        protected Duel duel => Duel.Current;
    }
}