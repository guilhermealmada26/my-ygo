namespace BBG.Campaign
{
    public interface IActionContainer
    {
        void Invoke();
        CampaignAction[] Actions { get; }
    }
}