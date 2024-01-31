namespace BBG.Dueling.Actions
{
    public class SetSummon : NormalSummon
    {
        public SetSummon(Monster monster) : base(monster)
        {
            position = CardPosition.DefFaceDown;
        }
    }
}