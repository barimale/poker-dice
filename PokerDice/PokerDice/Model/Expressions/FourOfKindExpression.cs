using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model.Expressions
{
    public class FourOfKindExpression : IExpression
    {
        public DiceResult? Interpret(int[] dice)
        {
            var result = dice.GroupBy(x => x).FirstOrDefault(p => p.Count() == 4);
            if (result == null)
                return null;

            return new DiceResult()
            {
                Type = DiceType.FourOfKind,
                Result = 45 //dice.GroupBy(x => x).First(p => p.Count() == 4).Sum()* 4
            };
        }
    }

}
