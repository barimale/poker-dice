using Iterator.Model.Collections;
using PokerDice.AI.DataModel;
using System.Collections.Concurrent;

namespace PokerDice.AI.Training
{
    public class Training
    {
        private PokerDiceEngine.PokerDiceEngine engine = new PokerDiceEngine.PokerDiceEngine();

        public Action<int, double, string, int[]> OnIterateChange;

        private readonly string[] AllMasks =
            Enumerable.Range(0, 32)
                .Select(i => Convert.ToString(i, 2).PadLeft(5, '0')
                    .Replace('0', 'K') // keep
                    .Replace('1', 'R')) // reroll
                .ToArray();

        public IEnumerable<DiceState> GenerateTrainingData(int samples)
        {
            ConcurrentBag<DiceState> total = new ConcurrentBag<DiceState>();

            Parallel.For(0, samples, new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            }, i =>
            {
                var dice = engine.SourceGenerator.Generate().Dice;
                var rollIndex = engine.SourceGenerator.GenerateRollIndex();

                // Decide best mask for this state, e.g. "KRRRK"
                string bestAction = ComputeBestAction(dice, rollIndex);

                if (!total.Contains(new DiceState
                {
                    Die1 = dice[0],
                    Die2 = dice[1],
                    Die3 = dice[2],
                    Die4 = dice[3],
                    Die5 = dice[4],
                    RollIndex = rollIndex,
                    Action = bestAction
                }, new DiceStateComparer()))
                {
                    OnIterateChange?.Invoke(i, (double)total.Count / samples * 100, bestAction, dice);

                    total.Add(new DiceState
                    {
                        Die1 = dice[0],
                        Die2 = dice[1],
                        Die3 = dice[2],
                        Die4 = dice[3],
                        Die5 = dice[4],
                        RollIndex = rollIndex,
                        Action = bestAction
                    });
                }
                else
                {
                    OnIterateChange?.Invoke(i, (double)total.Count / samples * 100, "REDUNDANT", dice);
                }
            });

            return total.AsEnumerable();
        }

        public IEnumerable<DiceState> GenerateTrainingData()
        {
            ConcurrentBag<DiceState> total = new ConcurrentBag<DiceState>();
            var items = engine.SourceGenerator
                .GenerateCollection()
                .Select(p => new DiceState()
                {
                    Die1 = p[0],
                    Die2 = p[1],
                    Die3 = p[2],
                    Die4 = p[3],
                    Die5 = p[4]
                });

            var collection = new DiceStateCollection(items.ToArray());
            var iterator = collection.CreateIterator();

            var samples = collection.Count();
            Parallel.For(0, samples, new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            }, i =>
            {
                if (iterator.HasNext())
                {
                    var dice = iterator.Next().ToArray(); // engine.SourceGenerator.Generate().Dice;
                    var rollIndex = engine.SourceGenerator.GenerateRollIndex();

                    // Decide best mask for this state, e.g. "KRRRK"
                    string bestAction = ComputeBestAction(dice, rollIndex);

                    if (!total.Contains(new DiceState
                    {
                        Die1 = dice[0],
                        Die2 = dice[1],
                        Die3 = dice[2],
                        Die4 = dice[3],
                        Die5 = dice[4],
                        RollIndex = rollIndex,
                        Action = bestAction
                    }, new DiceStateComparer()))
                    {
                        OnIterateChange?.Invoke(i, (double)total.Count / samples * 100, bestAction, dice);

                        total.Add(new DiceState
                        {
                            Die1 = dice[0],
                            Die2 = dice[1],
                            Die3 = dice[2],
                            Die4 = dice[3],
                            Die5 = dice[4],
                            RollIndex = rollIndex,
                            Action = bestAction
                        });
                    }
                    else
                    {
                        OnIterateChange?.Invoke(i, (double)total.Count / samples * 100, "REDUNDANT", dice);
                    }
                }
            });

            return total.AsEnumerable();
        }

        public int Score(int[] dice)
        {
            var result = engine.Interpreter.InterpretToResult(dice);

            return result.Result;
        }

        public double ExpectedValue(int[] dice, string mask, int rollIndex, int simulations = 100) // 2000
        {
            ConcurrentBag<int> total = new ConcurrentBag<int>();

            Parallel.For(0, simulations, new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            }, i =>
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
                    string nextMask = ComputeBestAction(d, rollIndex + 1, 1);
                    var scoretotal = (int)ExpectedValue(d, nextMask, rollIndex + 1, 1);
                    total.Add(scoretotal);
                }
                else
                {
                    total.Add(Score(d));
                }
            });

            return (double)total.Sum() / simulations;
        }

        private record ComputeMask(double bestValue, string bestMask);

        private string ComputeBestAction(int[] dice, int rollIndex, int simulations = 100)
        {
            ConcurrentBag<ComputeMask> results = new ConcurrentBag<ComputeMask>()
            {
                new ComputeMask(double.NegativeInfinity, "KKKKK") // baseline: keep all
            };

            Parallel.ForEach(
                AllMasks,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                },
                (mask =>
            {
                double ev = ExpectedValue(dice, mask, rollIndex, simulations); // Monte Carlo here
                results.Add(new ComputeMask(ev, mask));
            }));

            return results.MaxBy(p => p.bestValue).bestMask;
        }
    }
}
