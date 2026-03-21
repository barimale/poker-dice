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

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            var c = sender as Control;
            //c.BackColor = Color./*IndianRed*/;
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            var c = sender as Control;
            //c.BackColor = /*SystemColors*/.Control;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Poker Dice Game";
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.startButton.EnabledChanged += (s, ev) =>
            {
                if (startButton.Enabled)
                {
                    startButton.BackColor = Color.Green;
                    startButton.ForeColor = Color.White;
                    startButton.Font = new Font(
                        startButton.Font,
                        FontStyle.Bold
                    );
                }
                else
                {
                    startButton.BackColor = SystemColors.ControlDark;
                    startButton.ForeColor = SystemColors.ControlText;
                    startButton.Font = new Font(
                        startButton.Font,
                        FontStyle.Regular
                    );
                }
            };

            this.round2Button.EnabledChanged += (s, ev) =>
            {
                if (round2Button.Enabled)
                {
                    round2Button.BackColor = Color.Green;
                    round2Button.ForeColor = Color.White;
                }
                else
                {
                    round2Button.BackColor = SystemColors.ControlDark;
                    round2Button.ForeColor = SystemColors.ControlText;
                }
            };

            this.solveButton.EnabledChanged += (s, ev) =>
            {
                if (solveButton.Enabled)
                {
                    solveButton.BackColor = Color.Red;
                    solveButton.ForeColor = Color.White;
                    solveButton.Font = new Font(
                        solveButton.Font,
                        FontStyle.Bold
                    );
                }
                else
                {
                    solveButton.BackColor = SystemColors.ControlDark;
                    solveButton.ForeColor = SystemColors.ControlText;
                    solveButton.Font = new Font(
                        solveButton.Font,
                        FontStyle.Regular
                    );
                }
            };

            this.round3Button.EnabledChanged += (s, ev) =>
            {
                if (round3Button.Enabled)
                {
                    round3Button.BackColor = Color.Green;
                    round3Button.ForeColor = Color.White;
                }
                else
                {
                    round3Button.BackColor = SystemColors.ControlDark;
                    round3Button.ForeColor = SystemColors.ControlText;
                }
            };

            this.resetButton.EnabledChanged += (s, ev) =>
            {
                if (resetButton.Enabled)
                {
                    resetButton.BackColor = Color.Blue;
                    resetButton.ForeColor = Color.White;
                }
                else
                {
                    resetButton.BackColor = SystemColors.ControlDark;
                    resetButton.ForeColor = SystemColors.ControlText;
                }
            };

            this.startButton.Enabled = true;
            this.round2Button.Enabled = false;
            this.round3Button.Enabled = false;
            this.solveButton.Enabled = false;
            this.resetButton.Enabled = false;
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var senderCheckBox = sender as CheckBox;
            if (senderCheckBox.Checked)
                senderCheckBox.BackColor = Color.Green;
            else
                senderCheckBox.BackColor = SystemColors.Control;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            context = engine.SourceGenerator.Generate();
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
                checkBox.Font = new Font(checkBox.Font.FontFamily, 18f);
                checkBox.Size = new Size(80, 80);
                checkBox.MouseEnter += Control_MouseEnter;
                checkBox.MouseLeave += Control_MouseLeave;
                checkBox.BackColor = SystemColors.Control;
                //checkBox.FlatStyle = FlatStyle.Flat;
                checkBox.CheckedChanged += checkBox1_CheckedChanged;

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

            // disable all checkboxes
            foreach (var checkbox in dicesPanel.Controls.OfType<CheckBox>())
            {
                checkbox.Enabled = false;
            }
            this.round3Button.Enabled = false;
        }
    }
}
