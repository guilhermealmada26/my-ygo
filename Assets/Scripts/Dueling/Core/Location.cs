using BBG.Ygo;

namespace BBG.Dueling
{
    public enum Location
    {
        Deck, ExtraDeck, Hand, Graveyard, Banished, MonsterZone, STZone, FieldZone
    }

    static class CardLocationExtensions
    {
        public static bool IsField(this Location location)
        {
            return location is Location.MonsterZone or Location.STZone or Location.FieldZone;
        }

        public static bool IsHandOrField(this Location location)
        {
            return location == Location.Hand || IsField(location);
        }

        public static void SetLocation(this Card card, Location newLocation, Player controller = null)
        {
            if (controller == null)
                controller = card.Controller;

            if (newLocation == Location.STZone && card.Type.subtype == Subtype.Field)
                newLocation = Location.FieldZone;
            else if (newLocation is Location.Deck or Location.Hand && card.data.GoesToExtraDeck)
                newLocation = Location.ExtraDeck;
            else if (newLocation == Location.ExtraDeck && !card.data.GoesToExtraDeck)
                newLocation = Location.Deck;

            card.controller.Current = controller;
            card.location.Current = newLocation;
            card.controller.Previous[card.location.Previous].Remove(card);
            card.controller.Current[card.location.Current].Add(card);
            //set postion after movement
            var position = card.position;
            switch (card.Location)
            {
                case Location.Hand:
                    position.Current = card.Controller.Control == ControlMode.Manual ?
                        CardPosition.AtkFaceUp : CardPosition.AtkFaceDown;
                    break;

                case Location.Graveyard:
                case Location.Banished:
                    position.Current = CardPosition.AtkFaceUp;
                    break;

                case Location.Deck:
                case Location.ExtraDeck:
                    position.Current = CardPosition.AtkFaceDown;
                    break;
            }
        }
    }
}