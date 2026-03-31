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
            var fileName = "r:\\model.zip";
            File.Delete(fileName);

            //when
            new CreateModelAndUseIt().Main();

            //then
            Assert.True(File.Exists(fileName));
        }
    }
}
