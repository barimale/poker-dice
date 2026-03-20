using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model
{
    public class PokerDiceSourceGenerator
    {
        private readonly Random _rng = new Random();

        public DiceContext Generate()
        {
            int[] dice = Enumerable.Range(0, 5)
           .Select(_ => _rng.Next(1, 7))
           .ToArray();

            return new DiceContext(dice);
        }
    }
}
