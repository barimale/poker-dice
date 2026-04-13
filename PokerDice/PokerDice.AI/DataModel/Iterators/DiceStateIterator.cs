using Iterator.Model;
using PokerDice.AI.DataModel;

namespace Iterator.Model.Iterators
{
    public class DiceStateIterator : IIterator<DiceState>
    {
        private readonly DiceState[] _items;
        private int _position = 0;

        public DiceStateIterator(DiceState[] items)
        {
            _items = items;
        }

        public bool HasNext()
        {
            try
            {
                return _position < _items.Length && _items[_position] != null;
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
                return _items[_position++];
            }
            catch (Exception ex)
            {
                return _items[0];
            }
        }
    }

}
