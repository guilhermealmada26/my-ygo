using System;
using UnityEngine;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "Dialogue")]
    public class DialogueAction : CampaignAction
    {
        public Character character;
        [TextArea(2, 3)] public string text;

        protected override void OnInvoke(IActionContainer container)
        {
            ShowLine(true, container);
        }

        void ShowLine(bool callNext, IActionContainer c)
        {
            var header = character == null ? null : character.ChrName;
            var image = character == null ? null : character.DialogueImage;
            DialogueManager.Instance.ShowLine(GetText(), header, image, callNext ? () => InvokeNext(c) : null);
        }

        private string GetText()
        {
            var str = text;
            if (character != null)
                str = str.Replace("*", character.ChrName);
            return str;
        }
    }
}