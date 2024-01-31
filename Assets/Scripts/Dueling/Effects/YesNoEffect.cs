using BBG.Dueling.Selection;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    public class YesNoEffect : SimpleEffect
    {
        [TextArea(3, 3)]
        public string message;
        public Effect[] onYes;
        public Effect[] onNo;

        protected override void OnSetup()
        {
            onYes = onYes.Clone(card);
            onNo = onNo.Clone(card);
        }

        internal override void BeforeRemove()
        {
            base.BeforeRemove();
            onYes.Foreach(e => e.BeforeRemove());
            onNo.Foreach(e => e.BeforeRemove());
        }

        protected override void OnResolve()
        {
            if (onYes.Any(e => !e.CanResolve()))
            {
                onNo.Foreach(e => e.Resolve());
                return;
            }

            var selection = new YesNoSelection(player, message);
            selection.Perform();
            selection.AfterSelection += (s) =>
            {
                if (selection.choice.Value)
                    onYes.Foreach(e => e.Resolve());
                else
                    onNo.Foreach(e => e.Resolve());
            };
        }

        protected override void OnRevert()
        {
            onYes.Foreach(e => e.Revert());
            onNo.Foreach(e => e.Revert());
        }
    }
}