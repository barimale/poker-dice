using PokerDice.Abstraction;

namespace PokerDice.Model.Expressions
{
    public class HighDiceExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            return new DiceResult()
            {
                Type = DiceType.HighDice,
                Result = dice.Max()
            };
        }
    }

}
