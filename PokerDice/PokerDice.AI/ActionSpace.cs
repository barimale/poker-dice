using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDice.AI
{
    public static class ActionSpace
    {
        public static readonly bool[][] HoldMasks;

        static ActionSpace()
        {
            HoldMasks = new bool[32][];
            for (int mask = 0; mask < 32; mask++)
            {
                var arr = new bool[5];
                for (int i = 0; i < 5; i++)
                    arr[i] = (mask & (1 << i)) != 0;

                HoldMasks[mask] = arr;
            }
        }
    }

}
