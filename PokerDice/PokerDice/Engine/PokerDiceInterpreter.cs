using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;
using PokerDiceEngine.Model.Expressions;

namespace PokerDiceEngine.Engine
{
    public class PokerDiceInterpreter
    {
        private readonly HashSet<IExpression> _rules = new() // kolejnosc regul istotna
    {
        new PokerExpression(),
        new FourOfKindExpression(),
        new FullHouseExpression(),
        new StraightExpression(),
        new ThreeOfKindExpression(),
        new TwoPairsExpression(),
        new PairExpression(),
        new HighDiceExpression()
    };

        public DiceResult? Interpret(int[] dice)
        {
            foreach (var rule in _rules)
            {
                var result = rule.Interpret(dice);
                if (result != null)
                    return result;
            }

            return null;
        }
    }

}
