
public class MinMaxCurrent
{
    private int current;

    public int Min { get; private set; }
    public int Max { get; private set; }

    public MinMaxCurrent(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public int Current
    {
        get { return current; }
        set
        {
            if (IsFull)
                current = Max;
            current = value;
        }
    }

    public bool IsFull => current >= Max;

    public void SetToMinimun()
    {
        Current = Min;
    }
}
