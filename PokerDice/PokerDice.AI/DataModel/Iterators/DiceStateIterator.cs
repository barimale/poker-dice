using Iterator.Model;
using PokerDice.AI.DataModel;

namespace Iterator.Model.Iterators
{
    public class DiceStateIterator : IIterator<DiceState>
    {
        private readonly DiceState[] _books;
        private int _position = 0;

        public DiceStateIterator(DiceState[] books)
        {
            _books = books;
        }

        public bool HasNext()
        {
            try
            {
                return _position < _books.Length && _books[_position] != null;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public DiceState Next()
        {
            try
            {
                return _books[_position++];
            }
            catch (Exception ex)
            {
                return _books[0];
            }
        }
    }

}
