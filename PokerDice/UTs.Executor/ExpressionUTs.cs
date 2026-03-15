using PokerDice.Engine;
using PokerDice.Model;

namespace UTs.Executor
{
    public class ExpressionUTs
    {
        private readonly Random _rng = new Random();
        private readonly PokerDiceInterpreter _interpreter = new PokerDiceInterpreter();

        public string DiceText { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;

        [Fact]
        public void DetectPokerAndReturnResults()
        {
            //given
            int[] dice = [1, 1, 1, 1, 1];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.Interpret(dice);

            //then
            Assert.Equal(DiceType.Poker, result.Type);
            Assert.Equal(55, result.Result);
        }

        [Fact]
        public void DetectFourOfKindAndReturnResults()
        {
            //given
            int[] dice = [1, 1, 1, 1, 6];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.Interpret(dice);

            //then
            Assert.Equal(DiceType.FourOfKind, result.Type);
            Assert.Equal(4, result.Result);
        }

        [Fact]
        public void DetectThreeOfKindAndReturnResults()
        {
            //given
            int[] dice = [1, 1, 1, 4, 6];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.Interpret(dice);

            //then
            Assert.Equal(DiceType.ThreeOfKind, result.Type);
            Assert.Equal(3, result.Result);
        }

        [Fact]
        public void DetectHighDiceAndReturnResults()
        {
            //given
            int[] dice = [1, 2, 4, 5, 6];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.Interpret(dice);

            //then
            Assert.Equal(DiceType.HighDice, result.Type);
            Assert.Equal(6, result.Result);
        }
    }
}
