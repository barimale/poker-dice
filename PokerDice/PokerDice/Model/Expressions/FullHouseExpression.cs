using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class FullHouseExpression : IExpression
    {
        public string? Interpret(int[] dice)
        {
            var groups = dice.GroupBy(x => x).Select(g => g.Count()).OrderBy(x => x).ToArray();
            return groups.SequenceEqual(new[] { 2, 3 }) ? "Full" : null;
        }
    }

}
