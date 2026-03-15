using PokerDice.Model;

namespace PokerDice.Abstraction
{
    public interface IExpression
    {
        DiceResult? Interpret(int[] dice);
    }

}
