using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Abstraction
{
    public interface IExpression
    {
        DiceResult? Interpret(int[] dice);
    }

}
