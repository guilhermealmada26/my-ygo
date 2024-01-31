using System;
using UnityEngine;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "Fade Screen")]
    public class FadeScreenAction : CampaignAction
    {
        public FadeScreen.Type fadeType;
        [Range(0, 5)]
        public float duration = 1;

        protected override void OnInvoke(IActionContainer c)
        {
            FadeScreen.Instance.Fade(fadeType, duration, () => InvokeNext(c));
        }
    }
}