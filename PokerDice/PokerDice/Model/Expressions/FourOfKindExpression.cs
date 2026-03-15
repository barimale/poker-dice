namespace PokerDice.Model.Expressions
{
    public class FourOfKindExpression : IExpression
    {
        public string Interpret(int[] dice)
        {
            return dice.GroupBy(x => x).Any(g => g.Count() == 4)
                ? "Kareta"
                : null;
        }
    }

}
