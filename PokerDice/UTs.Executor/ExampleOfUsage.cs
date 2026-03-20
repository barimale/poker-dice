namespace UTs.Executor
{
    public class ExampleOfUsage
    {
        public string DiceText { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;

        [Fact]
        public void Execute()
        {
            //given
            var engine = new PokerDiceEngine.PokerDiceEngine();
            var dice = engine.SourceGenerator.Generate();

            //when
            DiceText = engine.SourceGenerator.ToString();
            var result = engine.Interpreter.Interpret(dice.Dice);

            //then
        }
    }
}
