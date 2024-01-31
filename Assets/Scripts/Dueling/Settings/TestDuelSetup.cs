using BBG.Ygo;
using UnityEngine;

namespace BBG.Dueling.Settings
{
    public class TestDuelSetup : MonoBehaviour
    {
        [SerializeField] DuelConfiguration testConfiguration;
        [SerializeField] CardData[] mainCards;
        [SerializeField] CardData[] extraCards;

        private void Awake()
        {
            if (DuelInitializer.Configuration == null)
                Setup();
        }

        private void Setup()
        {
            testConfiguration.player.mainDeck = mainCards.GetIDs();
            testConfiguration.player.extraDeck = extraCards.GetIDs();
            testConfiguration.opponent.mainDeck = mainCards.GetIDs();
            testConfiguration.opponent.extraDeck = extraCards.GetIDs();
            testConfiguration.SetAsOverride("Testing");
        }
    }
}