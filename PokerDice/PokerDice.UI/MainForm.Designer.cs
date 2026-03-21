namespace PokerDice.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startButton = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            round2Button = new Button();
            round3Button = new Button();
            solveButton = new Button();
            label4 = new Label();
            label5 = new Label();
            resetButton = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            startButton.BackColor = Color.FromArgb(192, 0, 0);
            startButton.ForeColor = SystemColors.Control;
            startButton.Location = new Point(12, 12);
            startButton.Name = "button1";
            startButton.Size = new Size(200, 46);
            startButton.TabIndex = 0;
            startButton.Text = "START";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(72, 314);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(72, 370);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(72, 425);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 316);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 4;
            label1.Text = "Dice:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 373);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 5;
            label2.Text = "Result:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 428);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
            label3.TabIndex = 6;
            label3.Text = "Type:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(232, 61);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(402, 393);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // round2Button
            // 
            round2Button.BackColor = Color.FromArgb(192, 0, 0);
            round2Button.ForeColor = SystemColors.Control;
            round2Button.Location = new Point(12, 64);
            round2Button.Name = "round2Button";
            round2Button.Size = new Size(200, 46);
            round2Button.TabIndex = 8;
            round2Button.Text = "Round 2";
            round2Button.UseVisualStyleBackColor = false;
            // 
            // round3Button
            // 
            round3Button.BackColor = Color.FromArgb(192, 0, 0);
            round3Button.ForeColor = SystemColors.Control;
            round3Button.Location = new Point(12, 116);
            round3Button.Name = "round3Button";
            round3Button.Size = new Size(200, 46);
            round3Button.TabIndex = 9;
            round3Button.Text = "Round 3";
            round3Button.UseVisualStyleBackColor = false;
            // 
            // solveButton
            // 
            solveButton.BackColor = Color.FromArgb(192, 0, 0);
            solveButton.ForeColor = SystemColors.Control;
            solveButton.Location = new Point(12, 168);
            solveButton.Name = "solveButton";
            solveButton.Size = new Size(200, 46);
            solveButton.TabIndex = 10;
            solveButton.Text = "Solve";
            solveButton.UseVisualStyleBackColor = false;
            solveButton.Click += button4_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 273);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 11;
            label4.Text = "Solution:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(232, 25);
            label5.Name = "label5";
            label5.Size = new Size(134, 20);
            label5.TabIndex = 12;
            label5.Text = "Select dices to roll:";
            // 
            // resetButton
            // 
            resetButton.BackColor = Color.FromArgb(192, 0, 0);
            resetButton.ForeColor = SystemColors.Control;
            resetButton.Location = new Point(12, 220);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(200, 46);
            resetButton.TabIndex = 13;
            resetButton.Text = "RESTART";
            resetButton.UseVisualStyleBackColor = false;
            resetButton.Click += resetButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(642, 466);
            Controls.Add(resetButton);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(solveButton);
            Controls.Add(round3Button);
            Controls.Add(round2Button);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(startButton);
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button round2Button;
        private Button round3Button;
        private Button solveButton;
        private Label label4;
        private Label label5;
        private Button resetButton;
    }
}
