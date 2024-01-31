using BBG.Dueling.Selection;
using BBG.OptionsSelection;
using BBG.Ygo;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class ChangeAttributeEffect : CardsEffect
    {
        public MonsterAttribute[] attributes;

        Monster[] monsters;
        MonsterAttribute selected;

        MonsterAttribute[] GetAttributes() => attributes.Length > 0 ? attributes : CardValuesHolder.Attributes();

        internal override void Resolve(List<Card> cards)
        {
            OnRevert();
            monsters = cards.Cast<Monster>().ToArray();
            var attributes = GetAttributes();
            var options = new Option[attributes.Length];
            for (int i = 0; i < attributes.Length; i++)
                options[i] = new Option { text = attributes[i].attributeName };
            var selection = new OptionSelection(player, "Select an attribute:", options);
            selection.Perform();
            selection.AfterSelection += (s) =>
            {
                selected = attributes[selection.choice.Value];
                monsters.Foreach(m => m.AttributeMod.Add(selected));
            };
        }

        protected override void OnRevert()
        {
            base.OnRevert();
            if (selected == null || monsters == null)
                return;

            monsters.Foreach(m => m.AttributeMod.Remove(selected));
        }
    }
}