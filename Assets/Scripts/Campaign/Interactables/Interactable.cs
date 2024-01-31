using UnityEngine;

namespace BBG.Campaign
{
    public abstract class Interactable : MonoBehaviour
    {
        public void Invoke() => OnInvoke();
        protected abstract void OnInvoke();
    }
}