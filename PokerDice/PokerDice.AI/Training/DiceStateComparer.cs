using PokerDice.AI.DataModel;

namespace PokerDice.AI.Training
{
    public class DiceStateComparer : IEqualityComparer<DiceState>
    {
        public bool Equals(DiceState x, DiceState y)
            => x.Die1.Equals(y.Die1) &&
            x.Die2.Equals(y.Die2) &&
            x.Die3.Equals(y.Die3) &&
            x.Die4.Equals(y.Die4) &&
            x.Die5.Equals(y.Die5);

        public int GetHashCode(DiceState obj)
            => HashCode.Combine(obj.Die1, obj.Die2, obj.Die3, obj.Die4, obj.Die5);
    }
}
