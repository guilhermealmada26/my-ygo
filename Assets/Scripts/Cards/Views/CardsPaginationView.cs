using BBG.Pagination;
using System.Collections.Generic;

namespace BBG.Ygo.CardViews
{
    public class CardsPaginationView : MultipleCardsView, IPaginationController
    {
        public PaginationCollection Pagination { private set; get; } = new();

        protected override void Awake()
        {
            base.Awake();
            Pagination.OnPageChanged += UpdateUI;
        }

        protected override void SetCardsView(List<CardData> cards)
        {
            Pagination.SetItems(CardsCount, cards);
        }

        private void UpdateUI()
        {
            base.SetCardsView(Pagination.GetPage<CardData>());
        }
    }
}