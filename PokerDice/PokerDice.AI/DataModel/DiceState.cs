namespace PokerDice.AI.DataModel
{
    public class DiceState
    {
        // One-hot or raw values; here: raw values + roll index
        public float Die1 { get; set; }
        public float Die2 { get; set; }
        public float Die3 { get; set; }
        public float Die4 { get; set; }
        public float Die5 { get; set; }
        public float RollIndex { get; set; } // 1,2,3
        public string Action { get; set; }   // label: e.g. "KRRRK" (K=keep,R=reroll)
    }
}
