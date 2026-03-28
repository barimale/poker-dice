namespace PokerDice.AI
{
    public sealed class PokerDiceEnvironment
    {
        private readonly Random _rng = new();

        public GameState Reset()
        {
            int[] dice = RollAll();
            bool[] held = new bool[5];
            int rerollsLeft = 2;
            int phase = 0;

            return new GameState(dice, held, rerollsLeft, phase);
        }

        public (GameState nextState, float reward, bool done) Step(GameState state, int action)
        {
            int[] dice = ExtractDice(state);
            bool[] held = ExtractHeld(state);
            int rerollsLeft = ExtractRerolls(state);
            int phase = ExtractPhase(state);

            bool[] newHeld = ActionSpace.HoldMasks[action];

            if (rerollsLeft == 0)
            {
                float rewardFinal = EvaluateHand(dice);
                var terminalState = new GameState(dice, newHeld, 0, 2);
                return (terminalState, rewardFinal, true);
            }

            int[] newDice = new int[5];
            for (int i = 0; i < 5; i++)
                newDice[i] = newHeld[i] ? dice[i] : RollOne();

            int newRerolls = rerollsLeft - 1;
            int newPhase = phase + 1;

            float reward = EvaluatePartial(dice, newDice);

            if (newRerolls == 0)
            {
                float finalReward = EvaluateHand(newDice);
                reward += finalReward;
                var terminalState = new GameState(newDice, newHeld, 0, newPhase);
                return (terminalState, reward, true);
            }

            var nextState = new GameState(newDice, newHeld, newRerolls, newPhase);
            return (nextState, reward, false);
        }

        private int RollOne() => _rng.Next(1, 7);

        private int[] RollAll() => new[] { RollOne(), RollOne(), RollOne(), RollOne(), RollOne() };

        private int[] ExtractDice(GameState s)
        {
            int[] dice = new int[5];
            for (int i = 0; i < 5; i++)
                dice[i] = (int)(s.Features[i] * 5f) + 1;
            return dice;
        }

        private bool[] ExtractHeld(GameState s)
        {
            bool[] held = new bool[5];
            for (int i = 0; i < 5; i++)
                held[i] = s.Features[5 + i] > 0.5f;
            return held;
        }

        private int ExtractRerolls(GameState s) => (int)(s.Features[10] * 2f);
        private int ExtractPhase(GameState s) => (int)(s.Features[11] * 2f);

        public float EvaluateHand(int[] dice)
        {
            Array.Sort(dice);

            bool five = dice[0] == dice[4];
            bool four = dice[0] == dice[3] || dice[1] == dice[4];
            bool full = (dice[0] == dice[1] && dice[2] == dice[4]) ||
                        (dice[0] == dice[2] && dice[3] == dice[4]);
            bool straight = IsStraight(dice);
            bool three = dice[0] == dice[2] || dice[1] == dice[3] || dice[2] == dice[4];
            bool two = dice[0] == dice[1] || dice[1] == dice[2] || dice[2] == dice[3] || dice[3] == dice[4];

            if (five) return 50;
            if (four) return 40;
            if (full) return 25;
            if (straight) return 15;
            if (three) return 5;
            if (two) return 1;
            return 0;
        }

        private bool IsStraight(int[] d)
        {
            return (d[0] == 1 && d[1] == 2 && d[2] == 3 && d[3] == 4 && d[4] == 5) ||
                   (d[0] == 2 && d[1] == 3 && d[2] == 4 && d[3] == 5 && d[4] == 6);
        }

        private float EvaluatePartial(int[] oldDice, int[] newDice)
        {
            float oldScore = EvaluateHand(oldDice);
            float newScore = EvaluateHand(newDice);
            return Math.Max(0, newScore - oldScore);
        }
    }

}
