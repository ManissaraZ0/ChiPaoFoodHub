namespace FoodHubCustomerApp.UserControlPages
{
    partial class UserDetailPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserDetailPage));
            mainSplitContainer = new SplitContainer();
            navBarControl = new NavBarControl();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel1;
            mainSplitContainer.IsSplitterFixed = true;
            mainSplitContainer.Location = new Point(0, 0);
            mainSplitContainer.Margin = new Padding(0);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(navBarControl);
            mainSplitContainer.Size = new Size(1280, 720);
            mainSplitContainer.SplitterDistance = 70;
            mainSplitContainer.SplitterWidth = 1;
            mainSplitContainer.TabIndex = 0;
            // 
            // navBarControl
            // 
            navBarControl.BackColor = Color.Transparent;
            navBarControl.Dock = DockStyle.Fill;
            navBarControl.Location = new Point(0, 0);
            navBarControl.LogoImage = (Image)resources.GetObject("navBarControl.LogoImage");
            navBarControl.Margin = new Padding(0);
            navBarControl.MaximumSize = new Size(0, 70);
            navBarControl.MinimumSize = new Size(0, 70);
            navBarControl.Name = "navBarControl";
            navBarControl.Size = new Size(1280, 70);
            navBarControl.TabIndex = 0;
            // 
            // UserDetailPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(mainSplitContainer);
            Margin = new Padding(0);
            Name = "UserDetailPage";
            Size = new Size(1280, 720);
            mainSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer mainSplitContainer;
        private NavBarControl navBarControl;
    }
}
