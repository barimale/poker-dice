namespace PokerDice.Abstraction
{
    public interface IExpression
    {
        string? Interpret(int[] dice);
    }

}
