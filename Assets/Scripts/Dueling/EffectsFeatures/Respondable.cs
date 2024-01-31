using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/Respondable")]
    public class Respondable : OnLocationFeature
    {
        protected override void OnApply()
        {
            Duel.Current.GetComponent<ChainSystem>().AddRespondable(effect);
        }

        protected override void OnUnApply()
        {
            Duel.Current.GetComponent<ChainSystem>().RemoveRespondable(effect);
        }
    }
}