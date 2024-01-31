using BBG.Dueling.Selection;
using BBG.OptionsSelection;
using BBG.Ygo;
using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public class ChangeNameEffect : CardsEffect
    {
        public CardData[] cardNames = new CardData[1];

        Card[] cards;
        string selected;

        internal override void Resolve(List<Card> cards)
        {
            OnRevert();
            this.cards = cards.ToArray();
            if (cardNames.Length > 1)
            {
                var options = new Option[cardNames.Length];
                for (int i = 0; i < cardNames.Length; i++)
                    options[i] = new Option { text = cardNames[i].cardName };
                var selection = new OptionSelection(player, "Select a card name:", options);
                selection.Perform();
                selection.AfterSelection += (s) =>
                {
                    selected = cardNames[selection.choice.Value].cardName;
                    cards.Foreach(m => m.NameMods.Add(selected));
                };
            }
            else
            {
                selected = cardNames[0].cardName;
                cards.Foreach(m => m.NameMods.Add(selected));
            }
        }

        protected override void OnRevert()
        {
            base.OnRevert();
            if (selected == null || cards == null)
                return;

            cards.Foreach(m => m.NameMods.Remove(selected));
        }
    }
}