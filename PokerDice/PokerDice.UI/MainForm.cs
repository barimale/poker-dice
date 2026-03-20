namespace PokerDice.UI
{
    public partial class MainForm : Form
    {
        private PokerDiceEngine.PokerDiceEngine engine = new PokerDiceEngine.PokerDiceEngine();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Poker Dice Game";
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dice = engine.SourceGenerator.Generate();
            textBox1.Text = dice.ToString();

            var result = engine.Interpreter.Interpret(dice.Dice);
            textBox2.Text = result.Result.ToString();
            textBox3.Text = result.Type.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
