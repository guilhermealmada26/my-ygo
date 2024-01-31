namespace BBG.Dueling.Effects
{
    public class CannotChangeControl : CardActionModifier
    {
        protected override EventName Event => EventName.ChangeControlAction;
    }
}