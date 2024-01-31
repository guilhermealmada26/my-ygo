using BBG.Dueling.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.AI
{
    [CreateAssetMenu(menuName = "Duel/AI/SetCardsBehaviour")]
    public class SetCardsBehaviour : AIDeclareAspect
    {
        public override DuelAction GetAction(Player player)
        {
            var st = new List<Card>(player[Location.Hand]);
            st.Filter(c => c is not Monster);
            if(st.Count == 0)
                return null;
            return new SetCardAction(st.GetRandom());
        }
    }
}