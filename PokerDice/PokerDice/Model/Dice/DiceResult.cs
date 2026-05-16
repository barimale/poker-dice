namespace PokerDiceEngine.Model.Dice
{
    public class DiceResult
    {
        public DiceType Type { get; set; }
        public int Result { get; set; }

        public override string ToString()
        {
            switch(Type)
            {
                case(DiceType.HighDice):
                    return "High Dice";
                case (DiceType.TwoPairs):
                    return "Two Pairs";
                case (DiceType.Pair):
                    return "Pair";
                case (DiceType.SmallStraight):
                    return "Small Straight";
                case (DiceType.Full):
                    return "Full";
                case (DiceType.FourOfKind):
                    return "Four Of Kind";
                case (DiceType.Poker):
                    return "Poker";
                case (DiceType.LargeStraight):
                    return "Large Straight";
                case (DiceType.ThreeOfKind):
                    return "Three Of Kind";
                default:
                    return string.Empty;
            }
        }
    }
}
