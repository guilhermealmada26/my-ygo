namespace BBG.Numbers
{
    public enum Comparison
    {
        Equal, LessThan, LessEqual, GreaterThan, GreaterEqual
    }

    public static class NumberComparisonExtensions
    {
        public static bool IsValid(this Comparison operation, int value, int toCompare)
        {
            return operation switch
            {
                Comparison.LessThan => value < toCompare,
                Comparison.LessEqual => value <= toCompare,
                Comparison.GreaterThan => value > toCompare,
                Comparison.GreaterEqual => value >= toCompare,
                _ => value == toCompare,
            };
        }

        public static bool IsValid(this Comparison operation, float value, float otherValue)
        {
            return operation switch
            {
                Comparison.LessThan => value < otherValue,
                Comparison.LessEqual => value <= otherValue,
                Comparison.GreaterThan => value > otherValue,
                Comparison.GreaterEqual => value >= otherValue,
                _ => value == otherValue,
            };
        }
    }
}