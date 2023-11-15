namespace BOT_V2.Grapher
{
    partial class GrapherForm_V5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            TLP_Buttons = new TableLayoutPanel();
            tabControl1 = new TabControl();
            RSI = new TabPage();
            FP_RSI = new ScottPlot.FormsPlot();
            KDJ = new TabPage();
            FP_CoinPrice = new ScottPlot.FormsPlot();
            OBV = new TabPage();
            FP_OBV = new ScottPlot.FormsPlot();
            FP_KDJ = new ScottPlot.FormsPlot();
            tableLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            RSI.SuspendLayout();
            KDJ.SuspendLayout();
            OBV.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(TLP_Buttons, 0, 0);
            tableLayoutPanel1.Controls.Add(tabControl1, 0, 2);
            tableLayoutPanel1.Controls.Add(FP_CoinPrice, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(876, 702);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // TLP_Buttons
            // 
            TLP_Buttons.ColumnCount = 14;
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.142856F));
            TLP_Buttons.Dock = DockStyle.Fill;
            TLP_Buttons.Location = new Point(3, 3);
            TLP_Buttons.Name = "TLP_Buttons";
            TLP_Buttons.RowCount = 2;
            TLP_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TLP_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TLP_Buttons.Size = new Size(870, 64);
            TLP_Buttons.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(RSI);
            tabControl1.Controls.Add(KDJ);
            tabControl1.Controls.Add(OBV);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 389);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(870, 310);
            tabControl1.TabIndex = 1;
            // 
            // RSI
            // 
            RSI.Controls.Add(FP_RSI);
            RSI.Location = new Point(4, 24);
            RSI.Name = "RSI";
            RSI.Padding = new Padding(3);
            RSI.Size = new Size(862, 282);
            RSI.TabIndex = 0;
            RSI.Text = "RSI";
            RSI.UseVisualStyleBackColor = true;
            // 
            // FP_RSI
            // 
            FP_RSI.Dock = DockStyle.Fill;
            FP_RSI.Location = new Point(3, 3);
            FP_RSI.Margin = new Padding(4, 3, 4, 3);
            FP_RSI.Name = "FP_RSI";
            FP_RSI.Size = new Size(856, 276);
            FP_RSI.TabIndex = 0;
            // 
            // KDJ
            // 
            KDJ.Controls.Add(FP_KDJ);
            KDJ.Location = new Point(4, 24);
            KDJ.Name = "KDJ";
            KDJ.Padding = new Padding(3);
            KDJ.Size = new Size(862, 282);
            KDJ.TabIndex = 1;
            KDJ.Text = "KDJ";
            KDJ.UseVisualStyleBackColor = true;
            // 
            // FP_CoinPrice
            // 
            FP_CoinPrice.Dock = DockStyle.Fill;
            FP_CoinPrice.Location = new Point(4, 73);
            FP_CoinPrice.Margin = new Padding(4, 3, 4, 3);
            FP_CoinPrice.Name = "FP_CoinPrice";
            FP_CoinPrice.Size = new Size(868, 310);
            FP_CoinPrice.TabIndex = 2;
            // 
            // OBV
            // 
            OBV.Controls.Add(FP_OBV);
            OBV.Location = new Point(4, 24);
            OBV.Name = "OBV";
            OBV.Padding = new Padding(3);
            OBV.Size = new Size(862, 282);
            OBV.TabIndex = 2;
            OBV.Text = "OBV";
            OBV.UseVisualStyleBackColor = true;
            // 
            // FP_OBV
            // 
            FP_OBV.Dock = DockStyle.Fill;
            FP_OBV.Location = new Point(3, 3);
            FP_OBV.Margin = new Padding(4, 3, 4, 3);
            FP_OBV.Name = "FP_OBV";
            FP_OBV.Size = new Size(856, 276);
            FP_OBV.TabIndex = 0;
            // 
            // FP_KDJ
            // 
            FP_KDJ.Dock = DockStyle.Fill;
            FP_KDJ.Location = new Point(3, 3);
            FP_KDJ.Margin = new Padding(4, 3, 4, 3);
            FP_KDJ.Name = "FP_KDJ";
            FP_KDJ.Size = new Size(856, 276);
            FP_KDJ.TabIndex = 0;
            // 
            // GrapherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(876, 702);
            Controls.Add(tableLayoutPanel1);
            Name = "GrapherForm";
            Text = "GrapherForm";
            tableLayoutPanel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            RSI.ResumeLayout(false);
            KDJ.ResumeLayout(false);
            OBV.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel TLP_Buttons;
        private TabControl tabControl1;
        private TabPage RSI;
        private TabPage KDJ;
        private ScottPlot.FormsPlot FP_CoinPrice;
        private ScottPlot.FormsPlot FP_RSI;
        private ScottPlot.FormsPlot FP_KDJ;
        private TabPage OBV;
        private ScottPlot.FormsPlot FP_OBV;
    }
}