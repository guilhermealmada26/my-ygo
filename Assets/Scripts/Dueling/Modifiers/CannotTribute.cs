namespace BBG.Dueling.Effects
{
    public class CannotTribute : CardActionModifier
    {
        protected override EventName Event => EventName.TributeAction;
    }
}