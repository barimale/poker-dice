namespace PokerDice.AI
{
    public sealed class DqnAgent
    {
        private readonly IQNetwork _qNetwork;
        private readonly ReplayBuffer _replay;
        private readonly int _actionCount;
        private readonly float _gamma;
        private readonly Random _rng = new();

        public float Epsilon { get; set; }

        public DqnAgent(IQNetwork qNetwork, int actionCount, int replayCapacity, float gamma, float initialEpsilon)
        {
            _qNetwork = qNetwork;
            _actionCount = actionCount;
            _replay = new ReplayBuffer(replayCapacity);
            _gamma = gamma;
            Epsilon = initialEpsilon;
        }

        public int SelectAction(GameState state)
        {
            if (_rng.NextDouble() < Epsilon)
                return _rng.Next(_actionCount);

            var q = _qNetwork.Predict(state);

            int best = 0;
            float bestVal = q[0];
            for (int i = 1; i < q.Length; i++)
            {
                if (q[i] > bestVal)
                {
                    bestVal = q[i];
                    best = i;
                }
            }
            return best;
        }

        public void StoreTransition(GameState s, int action, float reward, GameState sNext, bool done)
        {
            _replay.Add(new Transition(s, action, reward, sNext, done));
        }

        public void Train(int batchSize)
        {
            if (_replay.Count < batchSize)
                return;

            var batch = _replay.Sample(batchSize);

            var states = new float[batchSize][];
            var targetQs = new float[batchSize][];

            for (int i = 0; i < batchSize; i++)
            {
                var t = batch[i];

                var qCurrent = _qNetwork.Predict(t.State);
                var qNext = t.Done ? new float[_actionCount] : _qNetwork.Predict(t.NextState);

                float maxNext = 0f;
                if (!t.Done)
                {
                    maxNext = qNext[0];
                    for (int a = 1; a < _actionCount; a++)
                        if (qNext[a] > maxNext)
                            maxNext = qNext[a];
                }

                float target = t.Reward + (t.Done ? 0f : _gamma * maxNext);

                var qTarget = (float[])qCurrent.Clone();
                qTarget[t.Action] = target;

                states[i] = t.State.Features;
                targetQs[i] = qTarget;
            }

            _qNetwork.TrainBatch(states, targetQs);
        }
    }

}
