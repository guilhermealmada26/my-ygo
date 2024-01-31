namespace BBG.Numbers
{
    public enum Operation
    {
        Add, Subtract, Multiply, Divide, Set
    }

    public static class NumberOperationExtensions
    {
        public static int GetResult(this Operation operation, int value1, int value2)
        {
            return operation switch
            {
                Operation.Add => value1 + value2,
                Operation.Subtract => value1 - value2,
                Operation.Multiply => value1 * value2,
                Operation.Divide => value1 / value2,
                _ => value2,
            };
        }

        public static float GetResult(this Operation operation, float value1, float value2)
        {
            return operation switch
            {
                Operation.Add => value1 + value2,
                Operation.Subtract => value1 - value2,
                Operation.Multiply => value1 * value2,
                Operation.Divide => value1 / value2,
                _ => value2,
            };
        }

        public static Operation Reverse(this Operation operation)
        {
            return operation switch
            {
                Operation.Add => Operation.Subtract,
                Operation.Subtract => Operation.Add,
                Operation.Multiply => Operation.Divide,
                Operation.Divide => Operation.Multiply,
                _ => operation,
            };
        }
    }
}