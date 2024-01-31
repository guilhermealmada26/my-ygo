namespace BBG.Dueling.Selection
{
    public interface ICustomCardValue
    {
        int GetCount(CardsSelection selection, Card card);
    }
}