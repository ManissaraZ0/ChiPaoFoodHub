using FoodHubManagerApp.UserControlComponents;

namespace FoodHubManagerApp.UserControlPages
{
    partial class ReviewsPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReviewsPage));
            splitContainer1 = new SplitContainer();
            navBarControl1 = new NavBarControl();
            splitContainer2 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            sectionHeaderControl1 = new SectionHeaderControl();
            searchBar1 = new SearchBarControl();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            splitContainer2.Panel2.Controls.Add(flowLayoutPanel1);
            splitContainer2.Panel2.Padding = new Padding(20, 5, 20, 5);
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
            tableLayoutPanel1.Controls.Add(searchBar1, 1, 0);
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
            sectionHeaderControl1.HeaderText = "Reviews";
            sectionHeaderControl1.Location = new Point(3, 13);
            sectionHeaderControl1.Name = "sectionHeaderControl1";
            sectionHeaderControl1.Size = new Size(344, 55);
            sectionHeaderControl1.TabIndex = 0;
            // 
            // searchBar1
            // 
            searchBar1.Dock = DockStyle.Right;
            searchBar1.Location = new Point(468, 15);
            searchBar1.Margin = new Padding(0, 5, 10, 5);
            searchBar1.Name = "searchBar1";
            searchBar1.Size = new Size(518, 51);
            searchBar1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(20, 5);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0);
            flowLayoutPanel1.Size = new Size(966, 635);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.WrapContents = false;
            // 
            // ReviewsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Margin = new Padding(0);
            Name = "ReviewsPage";
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
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private UserControlComponents.NavBarControl navBarControl1;
        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel1;
        private SectionHeaderControl sectionHeaderControl1;
        private SearchBarControl searchBar1;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
