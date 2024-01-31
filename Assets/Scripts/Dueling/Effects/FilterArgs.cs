using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public class FilterArgs
    {
        public Card target;
        public Effect caller;
        public DuelAction action;

        public FilterArgs(Card target, Effect caller = null)
        {
            this.target = target;
            this.caller = caller;
        }
    }
}