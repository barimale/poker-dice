namespace PokerDice.Model
{
    public class DiceContext
    {
        public int[] Dice { get; }

        public DiceContext(int[] dice)
        {
            Dice = dice;
        }
    }

}
