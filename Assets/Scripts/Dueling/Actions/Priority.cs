namespace BBG.Dueling.Actions
{
    public enum Priority
    {
        Default = 0,
        AboveAll = 100,
        Auto = -30,
        Manual = -50,
        BelowDefault = -1,
        DelayedAction = -4,
		//chain ---------
        ChainLink = -5,
        ResolveChain = -6,
        ChainResolved = -7,
        SendResolvedCardsToGY = -8,
		BeforeAfterChain = -9,
        AfterChain = -10,
        //---------------
        AboveDefault = 2,
        Triggered = -12,
        AfterTriggers = -13,
    }
}