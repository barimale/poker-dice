using PokerDiceEngine.Abstraction;
using PokerDiceEngine.Model.Dice;
using PokerDiceEngine.Model.Expressions;

namespace PokerDiceEngine.Engine
{
    public class PokerDiceInterpreter: IHandEvaluator
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

        public DiceResult? InterpretToResult(int[] dice)
        {
            foreach (var rule in _rules)
            {
                var result = rule.Interpret(dice);
                if (result != null)
                    return result;
            }

            return null;
        }

        public int Interpret(int[] dice)
        {
            var result = InterpretToResult(dice);
            return result?.Result ?? 0;
        }
    }

}
