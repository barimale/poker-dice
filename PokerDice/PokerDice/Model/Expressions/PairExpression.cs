using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class PairExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var res = dice.GroupBy(x => x)
                        .FirstOrDefault(g => g.Count() == 2);

            if (res == null)
                return null;

            return dice
                .GroupBy(x => x)
                .Any(g => g.Count() == 2)
                ? new DiceResult()
                {
                    Type = DiceType.Pair,
                    Result = dice.GroupBy(x => x)
                        .First(g => g.Count() == 2)
                        .Sum()
                }
                : null;
        }
    }

}
