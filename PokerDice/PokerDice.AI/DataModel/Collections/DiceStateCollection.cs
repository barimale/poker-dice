using Iterator.Model.Iterators;
using PokerDice.AI.DataModel;

namespace Iterator.Model.Collections
{
    public class DiceStateCollection : IDiceStateCollection
    {
        private readonly DiceState[] _books;
        private int _index = 0;

        public DiceStateCollection(DiceState[] array)
        {
            _books = array;
        }

        public int Count()
        {
            return _books.Length;
        }

        public void Add(DiceState book)
        {
            if (_index < _books.Length)
                _books[_index++] = book;
        }

        public IIterator<DiceState> CreateIterator()
        {
            return new DiceStateIterator(_books);
        }
    }

}
