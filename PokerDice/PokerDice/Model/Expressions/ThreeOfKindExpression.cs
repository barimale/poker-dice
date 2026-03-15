using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class ThreeOfKindExpression : IExpression
    {
        public string? Interpret(int[] dice)
        {
            return dice.GroupBy(x => x).Any(g => g.Count() == 3)
                ? "Trójka"
                : null;
        }
    }

}
