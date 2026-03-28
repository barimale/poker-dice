namespace PokerDice.AI
{
    public interface IQNetwork
    {
        float[] Predict(GameState state);
        void TrainBatch(float[][] states, float[][] targetQs);
    }
}
