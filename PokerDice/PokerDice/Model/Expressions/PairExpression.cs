using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model.Expressions
{
    public class PairExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var pairs = dice
            .GroupBy(x => x)
            .Where(g => g.Count() == 2)
            .ToList();

            if (!pairs.Any())
                return null;

            return dice
                .GroupBy(x => x)
                .Any(g => g.Count() == 2)
                ? new DiceResult()
                {
                    Type = DiceType.Pair,
                    Result = pairs.First().Sum()
                }
                : null;
        }
    }

}
