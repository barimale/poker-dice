namespace PokerDice.AI.DataModel
{
    public sealed class DiceActionPrediction
    {
        public string PredictedAction { get; set; }
        public float[] Score { get; set; }
    }
}
