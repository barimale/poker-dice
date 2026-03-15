namespace PokerDice.Model.Expresions
{
    public class PairExpression : IExpression
    {
        public string Interpret(int[] dice)
        {
            return dice
                .GroupBy(x => x)
                .Any(g => g.Count() == 2)
                ? "Para"
                : null;
        }
    }

}
