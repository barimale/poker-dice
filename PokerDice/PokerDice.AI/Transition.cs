namespace PokerDice.AI
{
    public sealed class Transition
    {
        public GameState State { get; }
        public int Action { get; }
        public float Reward { get; }
        public GameState NextState { get; }
        public bool Done { get; }

        public Transition(GameState state, int action, float reward, GameState nextState, bool done)
        {
            State = state;
            Action = action;
            Reward = reward;
            NextState = nextState;
            Done = done;
        }
    }

}
