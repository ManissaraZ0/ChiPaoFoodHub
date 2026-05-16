namespace FoodHubCustomerApp.UserControlComponents
{
    partial class DialogCard
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
            tableLayoutPanel2 = new TableLayoutPanel();
            primaryBtn = new ButtonControl();
            secondaryBtn = new ButtonControl();
            tableLayoutPanel3 = new TableLayoutPanel();
            nofifyLabel = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42.1232872F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.7808228F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40.7534256F));
            tableLayoutPanel1.Size = new Size(499, 292);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(primaryBtn, 0, 0);
            tableLayoutPanel2.Controls.Add(secondaryBtn, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 175);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(10, 30, 10, 10);
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(493, 114);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // primaryBtn
            // 
            primaryBtn.BackColor = Color.Transparent;
            primaryBtn.ButtonColor = Color.White;
            primaryBtn.ButtonText = "OK";
            primaryBtn.Dock = DockStyle.Right;
            primaryBtn.FillMode = ButtonControl.ButtonStyle.Fill;
            primaryBtn.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            primaryBtn.FontColor = Color.FromArgb(111, 150, 12);
            primaryBtn.Location = new Point(91, 30);
            primaryBtn.Margin = new Padding(0);
            primaryBtn.MaximumSize = new Size(0, 50);
            primaryBtn.MinimumSize = new Size(140, 50);
            primaryBtn.Name = "primaryBtn";
            primaryBtn.Size = new Size(140, 50);
            primaryBtn.TabIndex = 1;
            primaryBtn.Click += btnOk_Click;
            // 
            // secondaryBtn
            // 
            secondaryBtn.BackColor = Color.Transparent;
            secondaryBtn.ButtonColor = Color.White;
            secondaryBtn.ButtonText = "Cancel";
            secondaryBtn.Dock = DockStyle.Left;
            secondaryBtn.FillMode = ButtonControl.ButtonStyle.Outline;
            secondaryBtn.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            secondaryBtn.FontColor = Color.FromArgb(111, 150, 12);
            secondaryBtn.Location = new Point(261, 30);
            secondaryBtn.Margin = new Padding(0);
            secondaryBtn.MaximumSize = new Size(0, 50);
            secondaryBtn.MinimumSize = new Size(140, 50);
            secondaryBtn.Name = "secondaryBtn";
            secondaryBtn.Size = new Size(140, 50);
            secondaryBtn.TabIndex = 2;
            secondaryBtn.Click += btnCancel_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel3.Controls.Add(nofifyLabel, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 126);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(493, 43);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // nofifyLabel
            // 
            nofifyLabel.AutoSize = true;
            nofifyLabel.Dock = DockStyle.Fill;
            nofifyLabel.Font = new Font("Segoe UI Semibold", 25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nofifyLabel.ForeColor = Color.White;
            nofifyLabel.Location = new Point(49, 0);
            nofifyLabel.Margin = new Padding(0);
            nofifyLabel.Name = "nofifyLabel";
            nofifyLabel.Size = new Size(394, 43);
            nofifyLabel.TabIndex = 0;
            nofifyLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(493, 117);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // DialogCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(111, 150, 12);
            ClientSize = new Size(499, 292);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DialogCard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AcceptCard";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private ButtonControl primaryBtn;
        private ButtonControl secondaryBtn;
        private TableLayoutPanel tableLayoutPanel3;
        private Label nofifyLabel;
        private TableLayoutPanel tableLayoutPanel4;
    }
}