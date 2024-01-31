using BBG.Conditions;
using BBG.Dueling.Actions;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    public abstract class Effect : ScriptableObject
    {
        public enum Type
        {
            Other, //face up effects, inner effects, triggered by others
            Activate,
            Cost,
            Trigger, //auto trigger, highest priority
            OptionalTrigger, //yes no trigger, middle priority
            ZoneActivate, //invocation, manual activate
            SpecialSummon, //summon action, like of cyber dragon
            EquipEffect  //effects applied to equipped monster
        }
        public Type type;
        public Feature[] features;
        public Effect cost;
        public Condition[] conditions;

        public Card card { private set; get; }
        public Player player => card.Controller;
        protected Duel duel => Duel.Current;
        internal virtual DuelAction TriggerAction { set; get; }

        public void Setup(Card card)
        {
            this.card = card;
            if (cost != null)
                cost = cost.Clone(card);
            features.Foreach(f => f.Setup(this));
            OnSetup();
        }

        protected virtual void OnSetup() { }

        public bool CanResolve()
        {
            if (cost != null && !cost.CanResolve())
                return false;
            if (!conditions.All(c => c.IsValid(this)))
                return false;
            //ignore specific conditions only
            if (features.Any(f => f is IgnoreConditions))
                return true;
            return IsResolvable();
        }

        protected virtual bool IsResolvable() => true;

        public virtual void OnActivate()
        {
            if (cost != null)
                cost.Resolve();
        }

        public void Resolve()
        {
            OnResolve();
            features.Foreach(f => f.OnResolve());
        }

        protected virtual void OnResolve() { }

        internal void Revert()
        {
            OnRevert();
            features.Foreach(f => f.OnRevert());
        }

        protected virtual void OnRevert() { }

        internal int SpeedSpell()
        {
            foreach (var feature in features)
                if (feature is CustomSpeedSpell speedSpell)
                    return speedSpell.value;
            if (card.Type.mainType == Ygo.MainType.Trap || features.Any((f) => f is Respondable))
                return 2;
            return 1;
        }

        internal bool ValidRespondable(Effect other)
        {
            if (features.Any(f => f is UnrespondableEffect))
                return false;
            return other.SpeedSpell() >= SpeedSpell();
        }

        internal virtual void BeforeRemove()
        {
            features.Foreach(f => f.OnRevert());
        }
    }
}