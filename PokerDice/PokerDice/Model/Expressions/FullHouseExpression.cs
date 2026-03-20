using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model.Expressions
{
    public class FullHouseExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var groups = dice.GroupBy(x => x).Select(g => g.Count()).OrderBy(x => x).ToArray();
            return groups.SequenceEqual(new[] { 2, 3 }) ? 
                new DiceResult()
                {
                    Type = DiceType.Full,
                    Result = dice.Sum()
                }
                : null;
        }
    }

}
