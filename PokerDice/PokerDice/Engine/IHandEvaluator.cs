namespace PokerDiceEngine.Engine
{
    public interface IHandEvaluator
    {
        public DiceResult? InterpretToResult(int[] dice);
        public int Interpret(int[] dice);
    }
}
