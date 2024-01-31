using System.Linq;
using UnityEngine;

namespace BBG.Campaign
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] GameObject[] observed;
        [SerializeField] CameraMovement2D cameraMovement;

        private void Start()
        {
            UpdateState();
        }

        public void UpdateState()
        {
            cameraMovement.enabled = observed.All(o => !o.activeInHierarchy);
        }
    }
}