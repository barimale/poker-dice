using PokerDiceEngine.Model.Dice;
using PokerDice.Generator; // namespace z Q#
using Microsoft.Quantum.Simulation.Simulators;

namespace PokerDiceEngine.Model
{
    public class PokerDiceSourceGenerator
    {
        private readonly Random _rng = new Random();
        private readonly QuantumSimulator _sim = new QuantumSimulator();

    public DiceContext Generate()
        {
            int[] dice = Enumerable.Range(0, 5)
               .Select(_ => GenerateDie())
               .ToArray();

            return new DiceContext(dice);
        }

        public IEnumerable<int[]> GenerateCollection()
        {
            var rolls =
                from a in Enumerable.Range(1, 6)
                from b in Enumerable.Range(1, 6)
                from c in Enumerable.Range(1, 6)
                from d in Enumerable.Range(1, 6)
                from e in Enumerable.Range(1, 6)
                select new[] { a, b, c, d, e };

            return rolls;
        }

        public int GenerateDie()
        {
            var result = RollDie.Run(_sim).Result;
            return (int)result;
        }

        public int GenerateRollIndex()
        {
            return _rng.Next(1, 4);
        }
    }
}
