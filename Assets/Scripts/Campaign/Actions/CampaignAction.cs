namespace BBG.Campaign
{
    [System.Serializable]
    public abstract class CampaignAction
    {
        public void Invoke(IActionContainer container)
        {
            OnInvoke(container);
        }

        protected abstract void OnInvoke(IActionContainer container);

        protected virtual void InvokeNext(IActionContainer container)
        {
            var actions = container.Actions;
            var index = actions.IndexOf(this);
            if (index < 0 || index >= actions.Length - 1)
                return;
            actions[index + 1]?.Invoke(container);
        }
    }
}