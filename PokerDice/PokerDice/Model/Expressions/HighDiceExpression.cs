using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model.Expressions
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
