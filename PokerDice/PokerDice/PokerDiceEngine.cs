using PokerDiceEngine.Engine;
using PokerDiceEngine.Model;

namespace PokerDiceEngine
{
    public class PokerDiceEngine
    {
        public PokerDiceSourceGenerator SourceGenerator { get; } = new PokerDiceSourceGenerator();

        public PokerDiceInterpreter Interpreter  { get; } = new PokerDiceInterpreter();
    }
}