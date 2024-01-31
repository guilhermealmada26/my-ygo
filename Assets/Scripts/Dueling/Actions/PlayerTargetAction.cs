namespace BBG.Dueling.Actions
{
    public abstract class PlayerTargetAction : DuelAction
    {
        public Player player;

        protected PlayerTargetAction(Player player)
        {
            this.player = player;
        }

        protected override bool ValidPerform()
        {
            if (priority == Priority.Manual && !player.IsTurnPlayer)
                return false;
            return base.ValidPerform();
        }
    }
}