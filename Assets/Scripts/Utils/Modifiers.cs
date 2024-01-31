using System;
using System.Collections.Generic;

public class Modifiers<T>
{
    private List<T> values = new(3);
    private T original;

    public T Current => values.Count > 0 ? values.Last() : original;
    public event Action<T> Changed;

    public Modifiers(T original)
    {
        this.original = original;
    }

    public void Add(T value)
    {
        values.Add(value);
        Changed?.Invoke(Current);
    }

    public void Remove(T value)
    {
        values.Remove(value);
        Changed?.Invoke(Current);
    }

    public void Clear()
    {
        values.Clear();
        Changed?.Invoke(Current);
    }
}
