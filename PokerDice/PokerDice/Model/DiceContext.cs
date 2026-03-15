namespace PokerDice.Model
{
    public class DiceContext
    {
        public int[] Dice { get; private set; }

        public DiceContext(int[] dice)
        {
            Dice = dice;
        }

        public override string ToString()
        {
            return string.Join(", ", Dice);
        }
    }

}
