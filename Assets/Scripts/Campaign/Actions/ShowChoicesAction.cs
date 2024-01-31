using System;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "Show Choices")]
    public class ShowChoicesAction : CampaignAction
    {
        [Serializable]
        public class Pair
        {
            public string text;
            public ActionContainer actions;
        }
        public Pair[] choices;

        protected override void OnInvoke(IActionContainer container)
        {
            var texts = new string[choices.Length];
            for (int i = 0; i < choices.Length; i++)
            {
                texts[i] = choices[i].text;
            }
            OptionsManager.Instance.ShowChoices(texts, (i) =>
            {
                var actions = choices[i].actions;
                if (actions != null)
                    actions.Invoke();
            });
        }
    }
}