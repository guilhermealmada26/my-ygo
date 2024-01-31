using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using BBG.Ygo;
using System.Collections.Generic;

namespace BBG.Dueling
{
    [System.Serializable]
    public class Card
    {
        public readonly CardData data;
        public readonly string id;
        public readonly PreviousCurrent<Location> location;
        public readonly PreviousCurrent<CardPosition> position = new(CardPosition.AtkFaceDown);
        public readonly PreviousCurrent<Player> controller;
        public readonly List<Effect> effects = new(3);
        public readonly List<EquipAction> equips = new(3);
        public readonly Modifiers<string> NameMods;

        public CardType Type => data.type;
        public Location Location => location.Current;
        public Player Controller => controller.Current;
        public CardPosition Position => position.Current;

        internal MoveCardAction LastMove;
        internal EquipAction equippedTo;

        public Card(CardData data, Player player, string id)
        {
            this.data = data.Clone();
            this.data.OriginalData = data;
            this.id = id;
            location = new(data.GoesToExtraDeck ? Location.ExtraDeck : Location.Deck);
            controller = new(player);
            this.SetLocation(location.Current);
            data.effects.Foreach(e => AddEffect(e));
            NameMods = new Modifiers<string>(data.cardName);
            NameMods.Changed += (n) => this.data.cardName = n;
        }

        internal void AddEffect(Effect effect)
        {
            var clone = effect.Clone(this);
            effects.Add(clone);
        }

        internal void RemoveEffect(Effect effect)
        {
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                if (effects[i].name == effect.name)
                {
                    effects[i].BeforeRemove();
                    effects.RemoveAt(i);
                    return;
                }
            }
        }
    }
}