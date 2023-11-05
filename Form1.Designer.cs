namespace BOT_V2
{
    partial class Form1
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
            button1 = new Button();
            B_BTCUSDT = new Button();
            comboBox1 = new ComboBox();
            LB_interval = new ListBox();
            button2 = new Button();
            TB_coinName = new TextBox();
            panel1 = new Panel();
            label1 = new Label();
            NUD_RSI = new NumericUpDown();
            panel2 = new Panel();
            label2 = new Label();
            NUD_getProfit = new NumericUpDown();
            panel3 = new Panel();
            label3 = new Label();
            NUD_stopLoss = new NumericUpDown();
            Button_Setup = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_RSI).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_getProfit).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_stopLoss).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // B_BTCUSDT
            // 
            B_BTCUSDT.Location = new Point(12, 41);
            B_BTCUSDT.Name = "B_BTCUSDT";
            B_BTCUSDT.Size = new Size(75, 23);
            B_BTCUSDT.TabIndex = 1;
            B_BTCUSDT.Text = "BTCUSDT";
            B_BTCUSDT.UseVisualStyleBackColor = true;
            B_BTCUSDT.Click += B_BTCUSDT_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(49, 157);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 2;
            // 
            // LB_interval
            // 
            LB_interval.FormattingEnabled = true;
            LB_interval.ItemHeight = 15;
            LB_interval.Items.AddRange(new object[] { "1m", "5m", "15m", "1h", "2h", "4h", "6h", "8h", "12h", "1d", "3d", "1w" });
            LB_interval.Location = new Point(12, 366);
            LB_interval.Name = "LB_interval";
            LB_interval.Size = new Size(120, 184);
            LB_interval.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(138, 527);
            button2.Name = "button2";
            button2.Size = new Size(119, 23);
            button2.TabIndex = 4;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // TB_coinName
            // 
            TB_coinName.Location = new Point(138, 366);
            TB_coinName.Name = "TB_coinName";
            TB_coinName.Size = new Size(227, 23);
            TB_coinName.TabIndex = 5;
            TB_coinName.Text = "BTCUSDT";
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(NUD_RSI);
            panel1.Location = new Point(138, 395);
            panel1.Name = "panel1";
            panel1.Size = new Size(230, 28);
            panel1.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(23, 15);
            label1.TabIndex = 1;
            label1.Text = "RSI";
            // 
            // NUD_RSI
            // 
            NUD_RSI.Location = new Point(107, 3);
            NUD_RSI.Name = "NUD_RSI";
            NUD_RSI.Size = new Size(120, 23);
            NUD_RSI.TabIndex = 0;
            NUD_RSI.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Controls.Add(NUD_getProfit);
            panel2.Location = new Point(138, 429);
            panel2.Name = "panel2";
            panel2.Size = new Size(230, 28);
            panel2.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 5);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 1;
            label2.Text = "getProfit";
            // 
            // NUD_getProfit
            // 
            NUD_getProfit.DecimalPlaces = 2;
            NUD_getProfit.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            NUD_getProfit.Location = new Point(107, 3);
            NUD_getProfit.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_getProfit.Name = "NUD_getProfit";
            NUD_getProfit.Size = new Size(120, 23);
            NUD_getProfit.TabIndex = 0;
            NUD_getProfit.Value = new decimal(new int[] { 11, 0, 0, 65536 });
            // 
            // panel3
            // 
            panel3.Controls.Add(label3);
            panel3.Controls.Add(NUD_stopLoss);
            panel3.Location = new Point(138, 463);
            panel3.Name = "panel3";
            panel3.Size = new Size(230, 28);
            panel3.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 5);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 1;
            label3.Text = "stopLoss";
            // 
            // NUD_stopLoss
            // 
            NUD_stopLoss.DecimalPlaces = 2;
            NUD_stopLoss.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            NUD_stopLoss.Location = new Point(107, 3);
            NUD_stopLoss.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NUD_stopLoss.Name = "NUD_stopLoss";
            NUD_stopLoss.Size = new Size(120, 23);
            NUD_stopLoss.TabIndex = 0;
            NUD_stopLoss.Value = new decimal(new int[] { 9, 0, 0, 65536 });
            // 
            // Button_Setup
            // 
            Button_Setup.Location = new Point(12, 337);
            Button_Setup.Name = "Button_Setup";
            Button_Setup.Size = new Size(353, 23);
            Button_Setup.TabIndex = 8;
            Button_Setup.Text = "Setup";
            Button_Setup.UseVisualStyleBackColor = true;
            Button_Setup.Click += Button_Setup_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(867, 562);
            Controls.Add(Button_Setup);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(TB_coinName);
            Controls.Add(button2);
            Controls.Add(LB_interval);
            Controls.Add(comboBox1);
            Controls.Add(B_BTCUSDT);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_RSI).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_getProfit).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_stopLoss).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button B_BTCUSDT;
        private ComboBox comboBox1;
        private ListBox LB_interval;
        private Button button2;
        private TextBox TB_coinName;
        private Panel panel1;
        private Label label1;
        private NumericUpDown NUD_RSI;
        private Panel panel2;
        private Label label2;
        private NumericUpDown NUD_getProfit;
        private Panel panel3;
        private Label label3;
        private NumericUpDown NUD_stopLoss;
        private Button Button_Setup;
    }
}