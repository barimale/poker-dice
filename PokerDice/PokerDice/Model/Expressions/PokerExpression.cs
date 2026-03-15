using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class PokerExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var res = dice.GroupBy(x => x).FirstOrDefault(g => g.Count() == 5);
            if (res == null)
                return null;

            return dice.GroupBy(x => x).Any(g => g.Count() == 5)
                ? new DiceResult()
                {
                    Type = DiceType.Poker,
                    Result = dice.GroupBy(x => x).First(g => g.Count() == 5).Sum() + 50
                }
                : null;
        }
    }

}
