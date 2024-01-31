using System.Linq;

namespace BBG.Conditions
{
    public class IfThenConditions : Condition
    {
        public Condition[] ifConditions, thenConditions;
        public bool defaultValue;

        public override bool IsValid(object args)
        {
            if (ifConditions.All(c => c.IsValid(args)))
            {
                return thenConditions.All(c => c.IsValid(args));
            }

            return defaultValue;
        }
    }
}