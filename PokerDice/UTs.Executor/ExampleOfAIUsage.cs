using PokerDice.AI;

namespace UTs.Executor
{
    public class ExampleOfAIUsage
    {
        public string DiceText { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;

        [Fact]
        public void Execute()
        {
            //given
            new CreateModelAndUseIt().Main();
            //when

            //then
        }
    }
}
