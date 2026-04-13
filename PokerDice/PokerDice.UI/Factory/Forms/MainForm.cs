using Microsoft.ML;
using PokerDice.AI.DataModel;
using PokerDice.UI.Features;
using PokerDiceEngine.Model.Dice;
using System.Drawing.Text;
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

            this.round2Button.EnabledChanged += (s, ev) =>
            {
                new MainButtonFeatures(this).ApplyButtonEnabledStyle(round2Button, Color.Green, changeFont: false);
            };

            this.solveButton.EnabledChanged += (s, ev) =>
            {
                new MainButtonFeatures(this).ApplyButtonEnabledStyle(solveButton, Color.Red, changeFont: true);
            };

            this.round3Button.EnabledChanged += (s, ev) =>
            {
                new MainButtonFeatures(this).ApplyButtonEnabledStyle(round3Button, Color.Green, changeFont: false);
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
            this.round2Button.Enabled = false;
            this.round3Button.Enabled = false;
            this.solveButton.Enabled = false;
            this.resetButton.Enabled = false;
            this.button1.Enabled = true;
            textBox2.Text = "";
            textBox3.Text = "";

            round1Button_Click(sender, e);
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

            this.startButton.Enabled = false;
            this.solveButton.Enabled = true;
            this.resetButton.Enabled = true;
            this.round2Button.Enabled = true;
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

        private void button4_Click(object sender, EventArgs e)
        {
            var result = engine.Interpreter.InterpretToResult(context.Dice);
            textBox2.Text = result.Result.ToString();
            textBox3.Text = result.Type.ToString();
            solveButton.Enabled = false;
            round2Button.Enabled = false;
            round3Button.Enabled = false;
            button1.Enabled = false;

            foreach (var checkbox in dicesPanel.Controls.OfType<CheckBox>())
            {
                checkbox.Checked = false;
                checkbox.Enabled = false;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
            dicesPanel.Controls.Clear();
            round1Button_Click(sender, e);
        }

        private void round2Button_Click(object sender, EventArgs e)
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

            this.round2Button.Enabled = false;
            this.round3Button.Enabled = true;
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

            new DiceFeatures(this).Apply();

            // disable all checkboxes
            foreach (var checkbox in dicesPanel.Controls.OfType<CheckBox>())
            {
                checkbox.Enabled = false;
            }
            this.round3Button.Enabled = false;
            button4_Click(sender, e);
        }

        private int ObtainRollIndex()
        {
            if (round2Button.Enabled == true)
                return 2;
            if (round3Button.Enabled == true)
                return 3;

            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var existedSolution = engine.Interpreter.InterpretToResult(context.Dice);

                if (existedSolution.Type == DiceType.Poker ||
                    existedSolution.Type == DiceType.FourOfKind ||
                    existedSolution.Type == DiceType.Full || 
                    existedSolution.Type == DiceType.LargeStraight ||
                    existedSolution.Type == DiceType.SmallStraight)
                {
                    button4_Click(sender, e);
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
                    button4_Click(sender, e);
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
            }catch(Exception)
            {
                // intentionally left blank
            }
        }
    }
}
