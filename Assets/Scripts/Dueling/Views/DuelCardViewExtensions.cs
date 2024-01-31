namespace BBG.Dueling.View
{
    public static class DuelCardViewExtensions
    {
        public static DuelCardView GetView(this Card card)
        {
            return DuelCardViewSetup.Get(card.id);
        }
    }
}
