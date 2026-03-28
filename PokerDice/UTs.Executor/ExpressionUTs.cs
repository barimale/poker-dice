using PokerDiceEngine.Engine;
using PokerDiceEngine.Model.Dice;

namespace UTs.Executor
{
    public class ExpressionUTs
    {
        private readonly PokerDiceEngine.PokerDiceEngine _engine;
        private readonly PokerDiceInterpreter _interpreter;

        public ExpressionUTs()
        {
            _engine = new PokerDiceEngine.PokerDiceEngine();
            _interpreter = _engine.Interpreter;
        }

        public string DiceText { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;

        [Fact]
        public void DetectPokerAndReturnResults()
        {
            //given
            int[] dice = [1, 1, 1, 1, 1];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.Poker, result.Type);
            Assert.Equal(60, result.Result);
        }

        [Fact]
        public void DetectFourOfKindAndReturnResults()
        {
            //given
            int[] dice = [5, 5, 5, 5, 6];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.FourOfKind, result.Type);
            Assert.Equal(45, result.Result);
        }

        [Fact]
        public void DetectThreeOfKindAndReturnResults()
        {
            //given
            int[] dice = [1, 1, 1, 4, 6];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.ThreeOfKind, result.Type);
            Assert.Equal(25, result.Result);
        }

        [Fact]
        public void DetectTwoPairsAndReturnResults()
        {
            //given
            int[] dice = [1, 2, 2, 5, 5];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.TwoPairs, result.Type);
            Assert.Equal(20, result.Result);
        }

        [Fact]
        public void DetectPairAndReturnResults()
        {
            //given
            int[] dice = [1, 2, 2, 5, 6];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.Pair, result.Type);
            Assert.Equal(10, result.Result);
        }

        [Fact]
        public void DetectHighDiceAndReturnResults()
        {
            //given
            int[] dice = [1, 2, 4, 5, 6];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.HighDice, result.Type);
            Assert.Equal(6, result.Result);
        }

        [Fact]
        public void DetectSmallStraightAndReturnResults()
        {
            //given
            int[] dice = [1, 2, 3, 4, 5];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.SmallStraight, result.Type);
            Assert.Equal(30, result.Result);
        }

        [Fact]
        public void DetectLargeStraightAndReturnResults()
        {
            //given
            int[] dice = [6, 2, 3, 4, 5];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.LargeStraight, result.Type);
            Assert.Equal(30, result.Result);
        }

        [Fact]
        public void DetectFullAndReturnResults()
        {
            //given
            int[] dice = [6, 6, 3, 3, 3];

            //when
            DiceText = string.Join(", ", dice);
            var result = _interpreter.InterpretToResult(dice);

            //then
            Assert.NotNull(result);
            Assert.Equal(DiceType.Full, result.Type);
            Assert.Equal(40, result.Result);
        }
    }
}
