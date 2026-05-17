using System.Windows.Forms;
using FoodHubManagerApp.UserControlComponents;

namespace FoodHubManagerApp.UserControlPages
{
    partial class AddPromotionPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPromotionPage));
            splitContainer1 = new SplitContainer();
            navBarControl1 = new NavBarControl();
            splitContainer2 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            sectionHeaderControl1 = new SectionHeaderControl();
            tableLayoutPanel2 = new TableLayoutPanel();
            labeledTextBoxControl4 = new LabeledTextBoxControl();
            labeledTextBoxControl1 = new LabeledTextBoxControl();
            labeledTextBoxControl2 = new LabeledTextBoxControl();
            labeledTextBoxControl3 = new LabeledTextBoxControl();
            tableLayoutPanel3 = new TableLayoutPanel();
            buttonControl1 = new ButtonControl();
            buttonControl2 = new ButtonControl();
            labeledDateTimePicker1 = new LabeledDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(navBarControl1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.White;
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1280, 720);
            splitContainer1.SplitterDistance = 270;
            splitContainer1.TabIndex = 0;
            // 
            // navBarControl1
            // 
            navBarControl1.Dock = DockStyle.Fill;
            navBarControl1.Location = new Point(0, 0);
            navBarControl1.LogoImage = (Image)resources.GetObject("navBarControl1.LogoImage");
            navBarControl1.Name = "navBarControl1";
            navBarControl1.SelectedIndex = 0;
            navBarControl1.Size = new Size(270, 720);
            navBarControl1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tableLayoutPanel2);
            splitContainer2.Size = new Size(1006, 720);
            splitContainer2.SplitterDistance = 71;
            splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(sectionHeaderControl1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 10, 10, 0);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1006, 71);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // sectionHeaderControl1
            // 
            sectionHeaderControl1.BackColor = Color.Transparent;
            sectionHeaderControl1.Dock = DockStyle.Fill;
            sectionHeaderControl1.HeaderText = "Add Promotion";
            sectionHeaderControl1.Location = new Point(3, 13);
            sectionHeaderControl1.Name = "sectionHeaderControl1";
            sectionHeaderControl1.Size = new Size(344, 55);
            sectionHeaderControl1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(labeledTextBoxControl4, 0, 4);
            tableLayoutPanel2.Controls.Add(labeledTextBoxControl1, 0, 0);
            tableLayoutPanel2.Controls.Add(labeledTextBoxControl2, 0, 1);
            tableLayoutPanel2.Controls.Add(labeledTextBoxControl3, 0, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 5);
            tableLayoutPanel2.Controls.Add(labeledDateTimePicker1, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(20, 30, 20, 0);
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel2.Size = new Size(1006, 645);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // labeledTextBoxControl4
            // 
            labeledTextBoxControl4.Dock = DockStyle.Fill;
            labeledTextBoxControl4.LabelText = "Price";
            labeledTextBoxControl4.Location = new Point(23, 417);
            labeledTextBoxControl4.Name = "labeledTextBoxControl4";
            labeledTextBoxControl4.PlaceholderText = "";
            labeledTextBoxControl4.Size = new Size(960, 164);
            labeledTextBoxControl4.TabIndex = 6;
            labeledTextBoxControl4.Value = "";
            // 
            // labeledTextBoxControl1
            // 
            labeledTextBoxControl1.Dock = DockStyle.Fill;
            labeledTextBoxControl1.LabelText = "Promotion Title";
            labeledTextBoxControl1.Location = new Point(23, 33);
            labeledTextBoxControl1.Name = "labeledTextBoxControl1";
            labeledTextBoxControl1.PlaceholderText = "";
            labeledTextBoxControl1.Size = new Size(960, 90);
            labeledTextBoxControl1.TabIndex = 0;
            labeledTextBoxControl1.Value = "";
            // 
            // labeledTextBoxControl2
            // 
            labeledTextBoxControl2.Dock = DockStyle.Fill;
            labeledTextBoxControl2.LabelText = "Promotion Conditions";
            labeledTextBoxControl2.Location = new Point(23, 129);
            labeledTextBoxControl2.Name = "labeledTextBoxControl2";
            labeledTextBoxControl2.PlaceholderText = "";
            labeledTextBoxControl2.Size = new Size(960, 90);
            labeledTextBoxControl2.TabIndex = 1;
            labeledTextBoxControl2.Value = "";
            // 
            // labeledTextBoxControl3
            // 
            labeledTextBoxControl3.Dock = DockStyle.Fill;
            labeledTextBoxControl3.LabelText = "Quota";
            labeledTextBoxControl3.Location = new Point(23, 225);
            labeledTextBoxControl3.Name = "labeledTextBoxControl3";
            labeledTextBoxControl3.PlaceholderText = "";
            labeledTextBoxControl3.Size = new Size(960, 90);
            labeledTextBoxControl3.TabIndex = 2;
            labeledTextBoxControl3.Value = "";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 650F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(buttonControl1, 1, 0);
            tableLayoutPanel3.Controls.Add(buttonControl2, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(20, 584);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(966, 61);
            tableLayoutPanel3.TabIndex = 4;
            // 
            // buttonControl1
            // 
            buttonControl1.BackColor = Color.Transparent;
            buttonControl1.ButtonColor = Color.Black;
            buttonControl1.ButtonText = "Cancel";
            buttonControl1.Dock = DockStyle.Fill;
            buttonControl1.FillMode = ButtonControl.ButtonStyle.Outline;
            buttonControl1.FontColor = Color.White;
            buttonControl1.Location = new Point(655, 0);
            buttonControl1.Margin = new Padding(5, 0, 5, 0);
            buttonControl1.MaximumSize = new Size(0, 40);
            buttonControl1.MinimumSize = new Size(140, 40);
            buttonControl1.Name = "buttonControl1";
            buttonControl1.Size = new Size(148, 40);
            buttonControl1.TabIndex = 0;
            buttonControl1.Click += buttonControl1_Load;
            // 
            // buttonControl2
            // 
            buttonControl2.BackColor = Color.Transparent;
            buttonControl2.ButtonColor = Color.FromArgb(192, 7, 7);
            buttonControl2.ButtonText = "Add";
            buttonControl2.Dock = DockStyle.Fill;
            buttonControl2.FillMode = ButtonControl.ButtonStyle.Fill;
            buttonControl2.FontColor = Color.White;
            buttonControl2.Location = new Point(808, 0);
            buttonControl2.Margin = new Padding(0);
            buttonControl2.MaximumSize = new Size(0, 40);
            buttonControl2.MinimumSize = new Size(140, 40);
            buttonControl2.Name = "buttonControl2";
            buttonControl2.Size = new Size(158, 40);
            buttonControl2.TabIndex = 1;
            buttonControl2.Click += buttonControl2_Load;
            // 
            // labeledDateTimePicker1
            // 
            labeledDateTimePicker1.BackColor = Color.Transparent;
            labeledDateTimePicker1.Dock = DockStyle.Fill;
            labeledDateTimePicker1.LabelText = "Expire Date";
            labeledDateTimePicker1.Location = new Point(23, 321);
            labeledDateTimePicker1.Name = "labeledDateTimePicker1";
            labeledDateTimePicker1.Size = new Size(960, 90);
            labeledDateTimePicker1.TabIndex = 5;
            labeledDateTimePicker1.Value = new DateTime(2026, 5, 17, 7, 49, 20, 222);
            // 
            // AddPromotionPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Margin = new Padding(0);
            Name = "AddPromotionPage";
            Size = new Size(1280, 720);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private UserControlComponents.NavBarControl navBarControl1;
        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel1;
        private SectionHeaderControl sectionHeaderControl1;
        private TableLayoutPanel tableLayoutPanel2;
        private LabeledTextBoxControl labeledTextBoxControl1;
        private LabeledTextBoxControl labeledTextBoxControl2;
        private LabeledTextBoxControl labeledTextBoxControl3;
        private TableLayoutPanel tableLayoutPanel3;
        private ButtonControl buttonControl1;
        private ButtonControl buttonControl2;
        private LabeledDateTimePicker labeledDateTimePicker1;
        private LabeledTextBoxControl labeledTextBoxControl4;
    }
}
