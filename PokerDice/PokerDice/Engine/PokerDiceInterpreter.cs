using PokerDice.Abstraction;
using PokerDice.Model.Expressions;

namespace PokerDice.Engine
{
    public class PokerDiceInterpreter
    {
        private readonly List<IExpression> _rules = new()
    {
        new PokerExpression(),
        new FullHouseExpression(),
        new StraightExpression(),
        new ThreeOfKindExpression(),
        new FourOfKindExpression(),
        new PairExpression()
    };

        public string Interpret(int[] dice)
        {
            foreach (var rule in _rules)
            {
                var result = rule.Interpret(dice);
                if (result != null)
                    return result;
            }

            return "Brak układu";
        }
    }

}
