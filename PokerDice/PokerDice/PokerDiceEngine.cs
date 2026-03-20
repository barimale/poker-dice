using PokerDiceEngine.Engine;
using PokerDiceEngine.Model;

namespace PokerDiceEngine
{
    public class PokerDiceEngine
    {
        public PokerDiceSourceGenerator SourceGenerator { get; private set; } = new PokerDiceSourceGenerator();

        public PokerDiceInterpreter Interpreter  { get; private set; } = new PokerDiceInterpreter();
    }
}