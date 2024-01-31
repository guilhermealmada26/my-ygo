using BBG.Dueling.Settings;
using UnityEngine;

namespace BBG.Dueling.AI
{
    [CreateAssetMenu(menuName = "Duel/Settings/AIDuelConfig")]
    public class AIDuelConfig : ConfigurationExtender
    {
        public AIContainer container;

        internal override void BeforeSetup(DuelConfiguration configuration, Duel duel)
        {
            FindObjectOfType<AIHandler>().SetAiContainer(container);
        }
    }
}