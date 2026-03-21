using PokerDiceEngine.Model.Dice;

namespace PokerDice.UI
{
    public partial class MainForm : Form
    {
        private PokerDiceEngine.PokerDiceEngine engine = new PokerDiceEngine.PokerDiceEngine();
        private DiceContext context;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Poker Dice Game";
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.startButton.Enabled = true;
            this.round2Button.Enabled = false;
            this.round3Button.Enabled = false;
            this.solveButton.Enabled = false;
            this.resetButton.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            context = engine.SourceGenerator.Generate();
            textBox1.Text = context.ToString();
            this.startButton.Enabled = false;
            this.solveButton.Enabled = true;
            this.resetButton.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = engine.Interpreter.Interpret(context.Dice);
            textBox2.Text = result.Result.ToString();
            textBox3.Text = result.Type.ToString();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
    }
}
