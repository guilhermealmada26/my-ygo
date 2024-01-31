using System.Collections;
using UnityEngine;

namespace BBG.Dueling.Settings
{
    public class DuelInitializer : MonoBehaviour
    {
        [SerializeField] DuelConfiguration defaultConfiguration;

        internal static DuelConfiguration Configuration { set; get; }

        private void Awake()
        {
            if (Configuration == null)
                Configuration = defaultConfiguration;
        }

        private IEnumerator Start()
        {
            var duel = Duel.Current;
            Configuration.Setup(duel);
            var components = FindObjectsOfType<DuelComponent>();
            foreach (var component in components)
                component.Setup(duel);
            yield return new WaitForSeconds(.5f);
            duel.StartDuel();
        }

        private void OnDestroy()
        {
            //reset on leave scene
            Configuration = null;
        }
    }
}