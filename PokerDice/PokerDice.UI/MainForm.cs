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
            this.startButton.BackColor = Color.Green;
            this.round2Button.BackColor = Color.White;
            this.round2Button.ForeColor = Color.Green;
            this.round3Button.BackColor = Color.White;
            this.round3Button.ForeColor = Color.Green;
            this.resetButton.BackColor = Color.Blue;
            this.resetButton.ForeColor = Color.White;
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
            // apply dice to checkboxes
            var index = 1;
            foreach(var d in context.Dice)
            {
                var checkBox = new CheckBox();
                checkBox.Name = index.ToString();
                checkBox.Text = d.ToString();
                checkBox.AutoSize = false;
                checkBox.Appearance = Appearance.Button;
                checkBox.TextAlign = ContentAlignment.MiddleCenter;
                checkBox.Size = new Size(80, 80);
                dicesPanel.Controls.Add(checkBox);
                index += 1;
            }

            this.startButton.Enabled = false;
            this.solveButton.Enabled = true;
            this.resetButton.Enabled = true;
            this.round2Button.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = engine.Interpreter.Interpret(context.Dice);
            textBox2.Text = result.Result.ToString();
            textBox3.Text = result.Type.ToString();
            solveButton.Enabled = false;
            round2Button.Enabled = false;
            round3Button.Enabled = false;

            foreach(var checkbox in dicesPanel.Controls.OfType<CheckBox>())
            {
                checkbox.Checked = false;
                checkbox.Enabled = false;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
            dicesPanel.Controls.Clear();
        }

        private void round2Button_Click(object sender, EventArgs e)
        {
            var anySelected = dicesPanel
                .Controls
                .OfType<CheckBox>()
                .Any(c => c.Checked);

            if(!anySelected)
            {
                return;
            }

            Execute();

            textBox1.Text = context.ToString();

            this.round2Button.Enabled = false;
            this.round3Button.Enabled = true;
        }

        private void Execute()
        {
            var selectedIndexes = dicesPanel
                            .Controls
                            .OfType<CheckBox>()
                            .Where(c => c.Checked)
                            .Select(c => int.Parse(c.Name))
                            .ToList();

            // re-roll selected dices
            foreach (var index in selectedIndexes)
            {
                context.Dice[index - 1] = engine.SourceGenerator.GenerateDie();
            }

            var unselectedIndexes = dicesPanel
                .Controls
                .OfType<CheckBox>()
                .Where(c => !c.Checked)
                .Select(c => int.Parse(c.Name))
                .ToList();

            // freeze not selected dices
            foreach (var unselected in unselectedIndexes)
            {
                var checkbox = dicesPanel.Controls.OfType<CheckBox>().First(c => c.Name == unselected.ToString());
                checkbox.Enabled = false;
            }

            // refresh enabled checkboxes text
            var unenabledCheckboxes =
                dicesPanel
                .Controls
                .OfType<CheckBox>()
                .Where(c => c.Enabled)
                .Select(c => int.Parse(c.Name))
                .ToList();

            foreach (var ue in unenabledCheckboxes)
            {
                var checkbox = dicesPanel.Controls.OfType<CheckBox>().First(c => c.Name == ue.ToString());
                checkbox.Text = context.Dice[ue - 1].ToString();
                checkbox.Checked = false;
            }
        }

        private void round3Button_Click(object sender, EventArgs e)
        {
            var anySelected = dicesPanel
                .Controls
                .OfType<CheckBox>()
                .Any(c => c.Checked);

            if (!anySelected)
            {
                return;
            }

            Execute();

            // display dice
            textBox1.Text = context.ToString();

            // disable all checkboxes
            foreach (var checkbox in dicesPanel.Controls.OfType<CheckBox>())
            {
                checkbox.Enabled = false;
            }
            this.round3Button.Enabled = false;
        }
    }
}
