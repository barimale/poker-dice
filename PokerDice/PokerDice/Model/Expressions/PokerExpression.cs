using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class PokerExpression : IExpression
    {
        public string? Interpret(int[] dice)
        {
            return dice.GroupBy(x => x).Any(g => g.Count() == 5)
                ? "Poker"
                : null;
        }
    }

}
