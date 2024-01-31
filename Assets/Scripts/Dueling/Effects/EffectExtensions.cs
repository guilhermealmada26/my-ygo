using UnityEngine;

namespace BBG.Dueling.Effects
{
    static class EffectExtensions
    {
        public static Effect Clone(this Effect effect, Card card)
        {
            if (effect == null)
                return null;
            var clone = Object.Instantiate(effect);
            clone.name = effect.name;
            for (int i = 0; i < clone.features.Length; i++)
            {
                clone.features[i] = Object.Instantiate(effect.features[i]);
            }
            clone.Setup(card);
            return clone;
        }

        public static T[] Clone<T>(this T[] effects, Card card) where T : Effect
        {
            var clones = new T[effects.Length];
            for (int i = 0; i < effects.Length; i++)
            {
                clones[i] = (T) effects[i].Clone(card);
            }
            return clones;
        }
    }
}