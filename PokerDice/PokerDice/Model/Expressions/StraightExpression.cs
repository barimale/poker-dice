using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class StraightExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var sorted = dice.OrderBy(x => x).ToArray();
            if (sorted.SequenceEqual(new[] { 1, 2, 3, 4, 5 }))
                return new DiceResult()
                {
                    Type = DiceType.SmallStraight,
                    Result = 15
                };
            if (sorted.SequenceEqual(new[] { 2, 3, 4, 5, 6 }))
                return new DiceResult()
                {
                    Type = DiceType.LargeStraight,
                    Result = 20,
                };

            return null;
        }
    }

}
