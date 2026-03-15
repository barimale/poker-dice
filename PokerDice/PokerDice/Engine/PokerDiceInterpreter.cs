using PokerDice.Abstraction;
using PokerDice.Model;
using PokerDice.Model.Expressions;

namespace PokerDice.Engine
{
    public class PokerDiceInterpreter
    {
        private readonly HashSet<IExpression> _rules = new() // kolejnosc ruli istotna
    {
        new PokerExpression(),
        new FourOfKindExpression(),
        new FullHouseExpression(),
        new StraightExpression(),
        new ThreeOfKindExpression(),
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
