namespace BBG.Dueling.Actions
{
    public class ShuffleAction : PlayerTargetAction
    {
        public Location location;
        public bool showAnimation;

        public ShuffleAction(Player player, Location location, bool showAnimation = true) : base(player)
        {
            this.location = location;
            this.showAnimation = showAnimation; 
        }

        protected override void OnPerform()
        {
            player[location].Shuffle(duel.Random);
        }
    }
}