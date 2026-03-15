using PokerDice.Engine;

namespace UTs.Executor
{
    public class ExampleOfUsage
    {
        private readonly Random _rng = new Random();
        private readonly PokerDiceInterpreter _interpreter = new PokerDiceInterpreter();

        public string DiceText { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;

        [Fact]
        public void Execute()
        {
            //given
            int[] dice = Enumerable.Range(0, 5)
           .Select(_ => _rng.Next(1, 7))
           .ToArray();

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.Interpret(dice);

            //then
        }
    }
}
