using PokerDice.Quantum;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using PokerDiceEngine.Model.Dice;

namespace PokerDiceEngine.Model
{
    public class PokerDiceSourceGenerator
    {
        private readonly Random _rng = new Random();

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
#if NET6_0
            using var sim = new QuantumSimulator();
            var result = RollQuantumDie.Run(sim).Result;
            return (int)result;
#else
    throw new PlatformNotSupportedException("Quantum operations require .NET 6");
#endif            
        }

        public int GenerateRollIndex()
        {
            return _rng.Next(1, 4);
        }
    }
}
