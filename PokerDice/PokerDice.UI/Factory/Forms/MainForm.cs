using Microsoft.ML;
using PokerDice.AI.DataModel;
using PokerDice.UI.Features;
using PokerDiceEngine.Model.Dice;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace PokerDice.UI
{
    public partial class MainForm : Form
    {
        private const string ModelPath = "r:\\model-XL.zip";

        private PrivateFontCollection pfc = new PrivateFontCollection();
        private Font diceFont;
        private Color colorAlreadySelected;
        private PredictionEngine<DiceState, DiceActionPrediction> aiEngine;

        public PokerDiceEngine.PokerDiceEngine engine = new PokerDiceEngine.PokerDiceEngine();
        public DiceContext context;

        private int selectedRoundNumber = 1;
        private bool IsAIActivated = false;

        public MainForm()
        {
            InitializeComponent();
            pfc.AddFontFile(@".\Resources\DpolyBlockDice.ttf");
            diceFont = new Font(pfc.Families[0], 40f);
            var ml = new MLContext();

            // Prediction engine
            // Define DataViewSchema and ITransformers
            DataViewSchema modelSchema;
            try
            {
                ITransformer trainedModel = ml.Model.Load(ModelPath, out modelSchema);

                aiEngine = ml.Model.CreatePredictionEngine<DiceState, DiceActionPrediction>(trainedModel); // model
            }
            catch (Exception)
            {
                // intentionally left blank
            }

            startButton.Text = "ROUND I";
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            var c = sender as Control;
            colorAlreadySelected = c.BackColor;
            c.BackColor = SystemColors.Highlight;
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            var c = sender as Control;
            c.BackColor = colorAlreadySelected;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Poker Dice Game";
            this.MinimizeBox = true;
            this.MaximizeBox = false;
            this.startButton.EnabledChanged += (s, ev) =>
            {
                new MainButtonFeatures(this).ApplyButtonEnabledStyle(startButton, Color.Green, changeFont: true);
            };

            this.solveButton.EnabledChanged += (s, ev) =>
            {
                new MainButtonFeatures(this).ApplyButtonEnabledStyle(solveButton, Color.Red, changeFont: true);
            };

            this.resetButton.EnabledChanged += (s, ev) =>
            {
                new MainButtonFeatures(this).ApplyButtonEnabledStyle(resetButton, Color.Blue, changeFont: false);
            };

            this.button1.EnabledChanged += (s, ev) =>
            {
                new MainButtonFeatures(this).ApplyButtonEnabledStyle(button1, Color.Fuchsia, changeFont: false);
                if (button1.Enabled == false)
                {
                    foreach (var checkBox in dicesPanel.Controls.OfType<CheckBox>())
                    {
                        checkBox.FlatStyle = FlatStyle.Standard;
                        checkBox.FlatAppearance.BorderSize = 1;
                        checkBox.FlatAppearance.BorderColor = SystemColors.ControlDark;
                    }
                }
            };

            this.startButton.Enabled = true;
            this.solveButton.Enabled = false;
            this.resetButton.Enabled = false;
            this.button1.Enabled = false;
            textBox2.Text = "";
            textBox3.Text = "";
            button1.Checked = false;
            this.startButton.Font = new Font(this.startButton.Font.FontFamily, 20, FontStyle.Bold);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var senderCheckBox = sender as CheckBox;
            if (senderCheckBox.Checked)
                senderCheckBox.BackColor = Color.Green;
            else
                senderCheckBox.BackColor = SystemColors.Control;

            colorAlreadySelected = senderCheckBox.BackColor;
        }

        private void round1Button_Click(object sender, EventArgs e)
        {
            var anySelected = dicesPanel
                            .Controls
                            .OfType<CheckBox>()
                            .Any(c => c.Checked);

            if (!anySelected && selectedRoundNumber != 1)
            {
                return;
            }

            if (selectedRoundNumber == 1)
            {
                RoundOneExecute();
                startButton.Text = "ROUND II";
                if (IsAIActivated)
                {
                    button1.Checked = true;
                }
                else
                {
                    button1.Checked = false;
                }
            }
            else if (selectedRoundNumber == 2)
            {
                RoundTwoExecute();
                startButton.Text = "ROUND III";
                if (IsAIActivated)
                {
                    button1.Checked = true;
                    AI_Click(sender, e);
                }
                else
                {
                    button1.Checked = false;
                }
            }
            else
            {
                RoundThreeExecute(sender, e);
            }
        }

        private void RoundOneExecute()
        {
            context = engine.SourceGenerator.Generate();
            // apply dice to checkboxes
            var index = 1;
            foreach (var d in context.Dice)
            {
                var checkBox = new CheckBox();
                checkBox.Name = index.ToString();
                checkBox.Text = d.ToString();
                checkBox.AutoSize = false;
                checkBox.Appearance = Appearance.Button;
                checkBox.TextAlign = ContentAlignment.MiddleCenter;
                checkBox.Font = diceFont;
                checkBox.Size = new Size(80, 80);
                checkBox.MouseEnter += Control_MouseEnter;
                checkBox.MouseLeave += Control_MouseLeave;
                checkBox.BackColor = SystemColors.Control;
                checkBox.CheckedChanged += checkBox1_CheckedChanged;
                checkBox.EnabledChanged += CheckBox_EnabledChanged;
                dicesPanel.Controls.Add(checkBox);
                index += 1;
            }

            this.solveButton.Enabled = true;
            this.resetButton.Enabled = true;
            this.button1.Enabled = true;

            selectedRoundNumber = 2;
        }

        private void CheckBox_EnabledChanged(object? sender, EventArgs e)
        {
            var senderCheckBox = sender as CheckBox;
            if (senderCheckBox.Enabled)
            {
                // intentionally left blank
            }
            else
            {
                senderCheckBox.BackColor = SystemColors.ControlDark;
                senderCheckBox.ForeColor = SystemColors.ControlText;
            }
        }

        private void solve_Click(object sender, EventArgs e)
        {
            var result = engine.Interpreter.InterpretToResult(context.Dice);
            textBox2.Text = result?.Result.ToString();
            textBox3.Text = result?.Type.ToString();
            solveButton.Enabled = false;
            button1.Enabled = false;

            foreach (var checkbox in dicesPanel.Controls.OfType<CheckBox>())
            {
                checkbox.Checked = false;
                checkbox.Enabled = false;
            }

            startButton.Enabled = false;
            IsAIActivated = false;
            button1.Checked = false;
            selectedRoundNumber = 1;
            button1.FlatStyle = FlatStyle.Standard;
            button1.FlatAppearance.BorderSize = 1;
            button1.FlatAppearance.BorderColor = SystemColors.ControlDark;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            dicesPanel.Controls.Clear();
            selectedRoundNumber = 1;
            startButton.Text = "ROUND I";
            button1.Enabled = false;
            button1.Checked = false;
            IsAIActivated = false;
            Form1_Load(sender, e);
        }

        private void round2Button_Click(object sender, EventArgs e)
        {
            RoundTwoExecute();
        }

        private void RoundTwoExecute()
        {
            var anySelected = dicesPanel
                            .Controls
                            .OfType<CheckBox>()
                            .Any(c => c.Checked);

            if (!anySelected)
            {
                return;
            }

            new DiceFeatures(this).Apply();

            ClearDiceBorders();

            selectedRoundNumber = 3;
        }

        private void round3Button_Click(object sender, EventArgs e)
        {
            RoundThreeExecute(sender, e);
        }

        private void RoundThreeExecute(object sender, EventArgs e)
        {
            var anySelected = dicesPanel
                            .Controls
                            .OfType<CheckBox>()
                            .Any(c => c.Checked);

            if (!anySelected)
            {
                return;
            }

            new DiceFeatures(this).Apply();

            // disable all checkboxes
            foreach (var checkbox in dicesPanel.Controls.OfType<CheckBox>())
            {
                checkbox.Enabled = false;
            }
            IsAIActivated = false;
            ClearDiceBorders();
            solve_Click(sender, e);
        }

        private int ObtainRollIndex()
        {
            return selectedRoundNumber;
        }

        private void AI_Click(object sender, EventArgs e)
        {
            if (!IsAIActivated)
            {
                foreach (var checkBox in dicesPanel.Controls.OfType<CheckBox>())
                {
                    if (checkBox != null)
                    {
                        checkBox.FlatStyle = FlatStyle.Standard;
                        checkBox.FlatAppearance.BorderSize = 1;
                        checkBox.FlatAppearance.BorderColor = SystemColors.ControlDark;
                    }
                }

                return;
            }
            try
            {
                var existedSolution = engine.Interpreter.InterpretToResult(context.Dice);

                if (existedSolution.Type == DiceType.Poker ||
                    existedSolution.Type == DiceType.Full ||
                    existedSolution.Type == DiceType.LargeStraight ||
                    existedSolution.Type == DiceType.SmallStraight)
                {
                    solve_Click(sender, e);
                    return;
                }

                var sample = new DiceState
                {
                    Die1 = context.Dice[0],
                    Die2 = context.Dice[1],
                    Die3 = context.Dice[2],
                    Die4 = context.Dice[3],
                    Die5 = context.Dice[4],
                    RollIndex = ObtainRollIndex(),
                };

                var pred = aiEngine.Predict(sample);

                if (pred.PredictedAction == "KKKKK")
                {
                    solve_Click(sender, e);
                }
                else
                {
                    var index = 1;
                    foreach (var suggestion in pred.PredictedAction.ToCharArray())
                    {
                        var checkBox = dicesPanel.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name == index.ToString());
                        if (checkBox != null)
                        {
                            if (suggestion == 'R')
                            {
                                checkBox.FlatStyle = FlatStyle.Flat;
                                checkBox.FlatAppearance.BorderSize = 5;
                                checkBox.FlatAppearance.BorderColor = Color.Fuchsia;
                            }
                            else // "K"
                            {
                                checkBox.FlatStyle = FlatStyle.Standard;
                                checkBox.FlatAppearance.BorderSize = 1;
                                checkBox.FlatAppearance.BorderColor = SystemColors.ControlDark;
                            }
                        }

                        index += 1;
                    }
                }
            }
            catch (Exception)
            {
                // intentionally left blank
            }
        }

        private void ClearDiceBorders()
        {
            foreach (var checkBox in dicesPanel.Controls.OfType<CheckBox>())
            {
                if (checkBox != null)
                {
                    checkBox.FlatStyle = FlatStyle.Standard;
                    checkBox.FlatAppearance.BorderSize = 1;
                    checkBox.FlatAppearance.BorderColor = SystemColors.ControlDark;
                }
            }
        }

        private void button1_CheckedChanged(object sender, EventArgs e)
        {
            var mapped = sender as CheckBox;
            if (!IsAIActivated && mapped.CheckState == CheckState.Unchecked)
                return;

            if (IsAIActivated && mapped.CheckState == CheckState.Checked)
                return;

            IsAIActivated = !IsAIActivated;
            if (IsAIActivated)
            {
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderSize = 5;
                button1.FlatAppearance.BorderColor = Color.Gold;
            }
            else
            {
                button1.FlatStyle = FlatStyle.Standard;
                button1.FlatAppearance.BorderSize = 1;
                button1.FlatAppearance.BorderColor = SystemColors.ControlDark;
            }

            AI_Click(sender, e);
        }
    }
}
