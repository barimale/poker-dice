namespace PokerDice.AI
{
    public sealed class GameState
    {
        public float[] Features { get; }

        public GameState(int[] dice, bool[] held, int rerollsLeft, int phase)
        {
            Features = new float[12];

            for (int i = 0; i < 5; i++)
                Features[i] = (dice[i] - 1) / 5f;

            for (int i = 0; i < 5; i++)
                Features[5 + i] = held[i] ? 1f : 0f;

            Features[10] = rerollsLeft / 2f;
            Features[11] = phase / 2f;
        }
    }

}
