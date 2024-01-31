using UnityEngine;

namespace BBG.Dueling.View
{
    public class LocationIcons : MonoBehaviour
    {
        [SerializeField] Sprite[] icons;

        private static LocationIcons instance;

        private void Awake()
        {
            instance = this;
        }

        public static Sprite Get(Location location)
        {
            return instance.icons[(int)location];
        }
    }
}
