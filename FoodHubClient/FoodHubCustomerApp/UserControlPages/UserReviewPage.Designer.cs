namespace FoodHubCustomerApp.UserControlPages
{
    partial class UserReviewPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserReviewPage));
            splitContainer1 = new SplitContainer();
            navBarControl1 = new NavBarControl();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
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
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(navBarControl1);
            splitContainer1.Size = new Size(1280, 720);
            splitContainer1.SplitterDistance = 70;
            splitContainer1.TabIndex = 0;
            // 
            // navBarControl1
            // 
            navBarControl1.BackColor = Color.Transparent;
            navBarControl1.Dock = DockStyle.Fill;
            navBarControl1.Location = new Point(0, 0);
            navBarControl1.LogoImage = (Image)resources.GetObject("navBarControl1.LogoImage");
            navBarControl1.Margin = new Padding(0);
            navBarControl1.MaximumSize = new Size(0, 70);
            navBarControl1.MinimumSize = new Size(0, 70);
            navBarControl1.Name = "navBarControl1";
            navBarControl1.Size = new Size(1280, 70);
            navBarControl1.TabIndex = 0;
            // 
            // UserReviewPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Margin = new Padding(0);
            Name = "UserReviewPage";
            Size = new Size(1280, 720);
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private NavBarControl navBarControl1;
    }
}
