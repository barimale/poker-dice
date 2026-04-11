using Iterator.Model;
using Iterator.Model.Iterators;
using PokerDice.AI.DataModel;

namespace Iterator.Model.Collections
{
    public interface IDiceStateCollection
    {
        IIterator<DiceState> CreateIterator();
    }

}
