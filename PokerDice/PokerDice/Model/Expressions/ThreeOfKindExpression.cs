using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model.Expressions
{
    public class ThreeOfKindExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var res = dice.GroupBy(x => x).FirstOrDefault(g => g.Count() == 3);
            if (res == null)
                return null;

            return dice.GroupBy(x => x).Any(g => g.Count() == 3)
                ? new DiceResult()
                {
                    Type = DiceType.ThreeOfKind,
                    Result = 25 //dice.GroupBy(x => x).First(g => g.Count() == 3).Sum()
                }
                : null;
        }
    }

}
