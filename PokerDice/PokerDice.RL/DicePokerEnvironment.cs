using PokerDiceEngine.Engine;
using PokerDiceEngine.Model.Dice;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDice.RL
{
    public class DicePokerEnvironment
    {
        private readonly Random _rng = new();
        private readonly IHandEvaluator _handEvaluator;

        public DicePokerEnvironment(IHandEvaluator handEvaluator)
        {
            _handEvaluator = handEvaluator;
        }

        public GameState Reset()
        {
            return new GameState(RollDice(), RerollsLeft: 2);
        }

        public (GameState next, int reward, bool done) Step(GameState state, int actionMask)
        {
            int[] dice = (int[])state.Dice.Clone();

            for (int i = 0; i < 5; i++)
            {
                bool reroll = (actionMask & (1 << i)) != 0;
                if (reroll)
                    dice[i] = _rng.Next(1, 7);
            }

            int rerollsLeft = state.RerollsLeft - 1;
            bool done = rerollsLeft == 0;
            int reward = done ? _handEvaluator.Interpret(dice) : 0;

            return (new GameState(dice, rerollsLeft), reward, done);
        }

        private int[] RollDice() =>
            Enumerable.Range(0, 5).Select(_ => _rng.Next(1, 7)).ToArray();
    }

}
