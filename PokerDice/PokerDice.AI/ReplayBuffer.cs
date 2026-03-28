namespace PokerDice.AI
{
    public sealed class ReplayBuffer
    {
        private readonly Transition[] _buffer;
        private int _index;
        private int _count;
        private readonly Random _rng = new();

        public int Count => _count;

        public ReplayBuffer(int capacity)
        {
            _buffer = new Transition[capacity];
        }

        public void Add(Transition t)
        {
            _buffer[_index] = t;
            _index = (_index + 1) % _buffer.Length;
            if (_count < _buffer.Length)
                _count++;
        }

        public List<Transition> Sample(int batchSize)
        {
            var result = new List<Transition>(batchSize);
            for (int i = 0; i < batchSize; i++)
            {
                int idx = _rng.Next(_count);
                result.Add(_buffer[idx]);
            }
            return result;
        }
    }

}
