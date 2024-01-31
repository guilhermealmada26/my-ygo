using System;

public class PreviousCurrent<T>
{
    private T current;

    public T Previous { get; private set; }
    public event Action ValueChanged;

    public PreviousCurrent(T current)
    {
        this.current = current;
    }

    public T Current
    {
        get => current;
        set
        {
            Previous = current;
            current = value;
            if (!SameValue)
                ValueChanged?.Invoke();
        }
    }

    public bool SameValue => current.Equals(Previous);
}
