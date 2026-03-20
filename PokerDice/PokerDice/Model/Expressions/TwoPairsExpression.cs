using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model.Expressions
{
    public class TwoPairsExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var pairs = dice
            .GroupBy(x => x)
            .Where(g => g.Count() == 2)
            .ToList();

            var groups = dice.GroupBy(x => x).Select(g => g.Count()).OrderBy(x => x).ToArray();
            return groups.SequenceEqual(new[] { 1, 2, 2 }) ? 
                new DiceResult()
                {
                    Type = DiceType.TwoPairs,
                    Result = pairs.Sum(p => p.Key * 2)
                }
                : null;
        }
    }

}
