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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            startButton = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            dicesPanel = new FlowLayoutPanel();
            round2Button = new Button();
            round3Button = new Button();
            solveButton = new Button();
            label4 = new Label();
            label5 = new Label();
            resetButton = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.BackColor = Color.Green;
            startButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            startButton.ForeColor = SystemColors.Control;
            startButton.Location = new Point(12, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(200, 46);
            startButton.TabIndex = 0;
            startButton.Text = "ROUND I";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += round1Button_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(294, 230);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(534, 230);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(234, 233);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 5;
            label2.Text = "Points:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(474, 233);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
            label3.TabIndex = 6;
            label3.Text = "Type:";
            // 
            // dicesPanel
            // 
            dicesPanel.Location = new Point(232, 61);
            dicesPanel.Name = "dicesPanel";
            dicesPanel.Size = new Size(437, 101);
            dicesPanel.TabIndex = 7;
            // 
            // round2Button
            // 
            round2Button.BackColor = Color.White;
            round2Button.ForeColor = Color.FromArgb(0, 192, 0);
            round2Button.Location = new Point(12, 64);
            round2Button.Name = "round2Button";
            round2Button.Size = new Size(200, 46);
            round2Button.TabIndex = 8;
            round2Button.Text = "ROUND II";
            round2Button.UseVisualStyleBackColor = false;
            round2Button.Click += round2Button_Click;
            // 
            // round3Button
            // 
            round3Button.BackColor = Color.White;
            round3Button.ForeColor = Color.FromArgb(0, 192, 0);
            round3Button.Location = new Point(12, 116);
            round3Button.Name = "round3Button";
            round3Button.Size = new Size(200, 46);
            round3Button.TabIndex = 9;
            round3Button.Text = "ROUND III";
            round3Button.UseVisualStyleBackColor = false;
            round3Button.Click += round3Button_Click;
            // 
            // solveButton
            // 
            solveButton.BackColor = Color.FromArgb(192, 0, 0);
            solveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            solveButton.ForeColor = SystemColors.Control;
            solveButton.Location = new Point(12, 168);
            solveButton.Name = "solveButton";
            solveButton.Size = new Size(200, 46);
            solveButton.TabIndex = 10;
            solveButton.Text = "SOLVE";
            solveButton.UseVisualStyleBackColor = false;
            solveButton.Click += button4_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(232, 181);
            label4.Name = "label4";
            label4.Size = new Size(88, 20);
            label4.TabIndex = 11;
            label4.Text = "SOLUTION:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(232, 25);
            label5.Name = "label5";
            label5.Size = new Size(139, 20);
            label5.TabIndex = 12;
            label5.Text = "Select dices to roll:";
            // 
            // resetButton
            // 
            resetButton.BackColor = Color.Blue;
            resetButton.ForeColor = SystemColors.Control;
            resetButton.Location = new Point(91, 220);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(121, 46);
            resetButton.TabIndex = 13;
            resetButton.Text = "NEXT GAME";
            resetButton.UseVisualStyleBackColor = false;
            resetButton.Click += resetButton_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Fuchsia;
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(12, 220);
            button1.Name = "button1";
            button1.Size = new Size(73, 46);
            button1.TabIndex = 14;
            button1.Text = "AI";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(675, 274);
            Controls.Add(button1);
            Controls.Add(resetButton);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(solveButton);
            Controls.Add(round3Button);
            Controls.Add(round2Button);
            Controls.Add(dicesPanel);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(startButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label2;
        private Label label3;
        public FlowLayoutPanel dicesPanel;
        private Button round2Button;
        private Button round3Button;
        private Button solveButton;
        private Label label4;
        private Label label5;
        private Button resetButton;
        private Button button1;
    }
}
