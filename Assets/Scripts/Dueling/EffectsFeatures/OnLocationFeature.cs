using System.Linq;

namespace BBG.Dueling.Effects
{
    public abstract class OnLocationFeature : Feature
    {
        public bool applyOnLeaveLocation;
        public Location[] locations;

        private bool applied;

        protected override void OnSetup()
        {
            effect.card.location.ValueChanged += LocationChanged;
            LocationChanged();
        }

        internal override void BeforeRemove()
        {
            effect.card.location.ValueChanged -= LocationChanged;
            LocationChanged();
        }

        private void LocationChanged()
        {
            if (locations.Length > 0)
            {
                if (locations.Contains(effect.card.Location))
                    Apply();
                else if (applied)
                    Revert();
            }
            else
            {
                Apply();
            }
        }

        private void Revert()
        {
            applied = false;
            if (applyOnLeaveLocation)
                OnApply();
            else
                OnUnApply();
        }

        private void Apply()
        {
            if (!applied)
            {
                applied = true;
                if (applyOnLeaveLocation)
                    OnUnApply();
                else
                    OnApply();
            }
        }

        protected abstract void OnApply();

        protected abstract void OnUnApply();
    }
}