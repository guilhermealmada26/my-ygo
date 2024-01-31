using BBG.Conditions;

namespace BBG.Dueling.Effects
{
    [System.Serializable]
    public class LocationGetter
    {
        public Location defaultLocation;
        public Pair[] alternatives;

        public Location Get(Card card)
        {
            foreach (var alt in alternatives)
                if (alt.condition.IsValid(card))
                    return alt.location;
            return defaultLocation;
        }

        [System.Serializable]
        public class Pair
        {
            public Location location;
            public Condition condition;
        }
    }
}