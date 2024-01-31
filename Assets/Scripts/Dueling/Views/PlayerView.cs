using BBG.CustomLayouts;
using UnityEngine;

namespace BBG.Dueling.View
{
    [RequireComponent(typeof(Player))]
    public class PlayerView : DuelComponent
    {
        [SerializeField] PlayerPortrait portrait;
        [SerializeField] SpriteRenderer background;
        [SerializeField] CustomLayout deck, extraDeck, hand, fieldZone, graveyard, banished;
        [SerializeField] CustomLayout[] monsters, spellsTraps;

        public GameObject highlightGraveyard, highlightBanished, highlightExtraDeck;

        public PlayerPortrait Portrait => portrait;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            var player = GetComponent<Player>();
            portrait.SetData(player);
        }

        public void Highlight(bool value)
        {
            portrait.Highlight(value);
        }

        public CustomLayout GetLocation(Location location)
        {
            return location switch
            {
                Location.Hand => hand,
                Location.Graveyard => graveyard,
                Location.Banished => banished,
                Location.Deck => deck,
                Location.ExtraDeck => extraDeck,
                Location.FieldZone => fieldZone,
                _ => FirstEmpty(location),
            };
        }

        private CustomLayout FirstEmpty(Location location)
        {
            var zones = location == Location.MonsterZone ? monsters : spellsTraps;
            foreach (var zn in zones)
                if (zn.transform.childCount == 0)
                    return zn;
            return null;
        }

        public void SetBackgorund(Sprite image) => background.sprite = image;
    }
}
