using UnityEngine;

namespace BBG.Campaign
{
    public class NpcInteraction : Interactable
    {
        [SerializeField] NpcBehaviour behaviour;

        void Start()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = behaviour.Img;
            behaviour.OnStart();
        }

        protected override void OnInvoke()
        {
            behaviour.OnInvoke();
        }
    }
}