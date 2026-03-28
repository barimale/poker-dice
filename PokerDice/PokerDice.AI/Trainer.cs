namespace PokerDice.AI
{
    public sealed class Trainer
    {
        private readonly PokerDiceEnvironment _env;
        private readonly DqnAgent _agent;

        public Trainer(PokerDiceEnvironment env, DqnAgent agent)
        {
            _env = env;
            _agent = agent;
        }

        public void Train(int episodes)
        {
            float movingAverage = 0f;

            for (int ep = 1; ep <= episodes; ep++)
            {
                var state = _env.Reset();
                bool done = false;
                float totalReward = 0f;

                while (!done)
                {
                    int action = _agent.SelectAction(state);

                    var (nextState, reward, isDone) = _env.Step(state, action);

                    _agent.StoreTransition(state, action, reward, nextState, isDone);
                    _agent.Train(batchSize: 64);

                    totalReward += reward;
                    state = nextState;
                    done = isDone;
                }

                movingAverage = movingAverage * 0.99f + totalReward * 0.01f;

                if (ep % 1000 == 0)
                {
                    Console.WriteLine($"Ep {ep} | Reward: {totalReward:F1} | Avg: {movingAverage:F2} | ε={_agent.Epsilon:F3}");
                }

                _agent.Epsilon = Math.Max(0.05f, _agent.Epsilon * 0.9995f);
            }
        }
    }

}
