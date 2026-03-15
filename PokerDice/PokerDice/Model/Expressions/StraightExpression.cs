using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class StraightExpression : IExpression
    {
        public string? Interpret(int[] dice)
        {
            var sorted = dice.OrderBy(x => x).ToArray();
            if (sorted.SequenceEqual(new[] { 1, 2, 3, 4, 5 }))
                return "Mały strit";
            if (sorted.SequenceEqual(new[] { 2, 3, 4, 5, 6 }))
                return "Duży strit";
            return null;
        }
    }

}
