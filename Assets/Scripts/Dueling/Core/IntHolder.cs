using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling
{
    public class IntHolder
    {
        public int Original { private set; get; }
        public int Current { private set; get; }

        private int min;
        readonly List<int> modifiers = new(3);
        public event System.Action ValueChanged;

        public IntHolder(int originial, int min = 0)
        {
            Original = originial;
            Current = originial;
            this.min = min;
        }

        private void Recalculate()
        {
            Current = Original;
            foreach (var mod in modifiers)
                Current += mod;
            Current = Mathf.Clamp(Current, min, 999999);
            ValueChanged?.Invoke();
        }

        public void Add(int modifier)
        {
            modifiers.Add(modifier);
            Recalculate();
        }

        public void Remove(int modifier)
        {
            modifiers.Remove(modifier);
            Recalculate();
        }

        public void Reset()
        {
            Set(Original);
        }

        public void Set(int value)
        {
            modifiers.Clear();
            Current = Mathf.Clamp(value, 0, 999999);
            ValueChanged?.Invoke();
        }
    }
}