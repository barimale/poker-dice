using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class FullHouseExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var groups = dice.GroupBy(x => x).Select(g => g.Count()).OrderBy(x => x).ToArray();
            return groups.SequenceEqual(new[] { 2, 3 }) ? 
                new DiceResult()
                {
                    Type = DiceType.Full,
                    Result = groups.Sum()
                }
                : null;
        }
    }

}
