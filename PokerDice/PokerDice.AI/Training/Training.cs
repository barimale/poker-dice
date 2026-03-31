using PokerDice.AI.DataModel;

namespace PokerDice.AI.Training
{
    public class Training
    {
        private static PokerDiceEngine.PokerDiceEngine engine = new PokerDiceEngine.PokerDiceEngine();

        public static readonly string[] AllMasks =
            Enumerable.Range(0, 32)
                .Select(i => Convert.ToString(i, 2).PadLeft(5, '0')
                    .Replace('0', 'K') // keep
                    .Replace('1', 'R')) // reroll
                .ToArray();

        // WIP dolozyc logike z pokerDice
        public static IEnumerable<DiceState> GenerateTrainingData(int samples)
        {
            var rnd = new Random();
            for (int i = 0; i < samples; i++)
            {
                var dice = engine.SourceGenerator.Generate().Dice;
                var rollIndex = engine.SourceGenerator.GenerateRollIndex();

                // Your existing heuristic / dynamic programming / EV calc:
                // Decide best mask for this state, e.g. "KRRRK"
                string bestAction = ComputeBestAction(dice, rollIndex);

                yield return new DiceState
                {
                    Die1 = dice[0],
                    Die2 = dice[1],
                    Die3 = dice[2],
                    Die4 = dice[3],
                    Die5 = dice[4],
                    RollIndex = rollIndex,
                    Action = bestAction
                };
            }
        }

        public static int Score(int[] dice)
        {
            var result = engine.Interpreter.InterpretToResult(dice);

            return result.Result;
        }

        public static double ExpectedValue(int[] dice, string mask, int rollIndex, int simulations = 10) // 2000
        {
            var rnd = new Random();
            int total = 0;

            for (int i = 0; i < simulations; i++)
            {
                var d = dice.ToArray();

                // Reroll dice marked 'R'
                for (int j = 0; j < 5; j++)
                {
                    if (mask[j] == 'R')
                        d[j] = engine.SourceGenerator.GenerateDie();
                }

                // If not final roll, recursively simulate future rolls
                if (rollIndex < 3)
                {
                    // Greedy: pick best mask for next roll
                    string nextMask = ComputeBestAction(d, rollIndex + 1);
                    total += (int)ExpectedValue(d, nextMask, rollIndex + 1, 1);
                }
                else
                {
                    total += Score(d);
                }
            }

            return (double)total / simulations;
        }


        private static string ComputeBestAction(int[] dice, int rollIndex)
        {
            double bestValue = double.NegativeInfinity;
            string bestMask = "KKKKK"; // default: keep all

            foreach (var mask in AllMasks)
            {
                double ev = ExpectedValue(dice, mask, rollIndex);

                if (ev > bestValue)
                {
                    bestValue = ev;
                    bestMask = mask;
                }
            }

            return bestMask;
        }
    }
}
