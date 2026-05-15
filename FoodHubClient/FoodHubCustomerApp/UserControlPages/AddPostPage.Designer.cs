namespace FoodHubCustomerApp.UserControlPages
{
    partial class AddPostPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPostPage));
            mainSplitContainer = new SplitContainer();
            navBarControl = new NavBarControl();
            contentSplitContainer = new SplitContainer();
            contentLeftPanel = new Panel();
            subSplitLeftContainer = new SplitContainer();
            headLeftLabel = new Label();
            subSubSplitLeftContainer = new SplitContainer();
            imageScreenControlNew = new FoodHubCustomerApp.UserControlComponents.ImageScreenControlNew();
            contentRightPanel = new Panel();
            subSplitRightContainer = new SplitContainer();
            rightSectionHeaderControl = new SectionHeaderControl();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).BeginInit();
            contentSplitContainer.Panel1.SuspendLayout();
            contentSplitContainer.Panel2.SuspendLayout();
            contentSplitContainer.SuspendLayout();
            contentLeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)subSplitLeftContainer).BeginInit();
            subSplitLeftContainer.Panel1.SuspendLayout();
            subSplitLeftContainer.Panel2.SuspendLayout();
            subSplitLeftContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)subSubSplitLeftContainer).BeginInit();
            subSubSplitLeftContainer.Panel1.SuspendLayout();
            subSubSplitLeftContainer.SuspendLayout();
            contentRightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)subSplitRightContainer).BeginInit();
            subSplitRightContainer.Panel1.SuspendLayout();
            subSplitRightContainer.SuspendLayout();
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
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(contentSplitContainer);
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
            // contentSplitContainer
            // 
            contentSplitContainer.Dock = DockStyle.Fill;
            contentSplitContainer.FixedPanel = FixedPanel.Panel1;
            contentSplitContainer.IsSplitterFixed = true;
            contentSplitContainer.Location = new Point(0, 0);
            contentSplitContainer.Margin = new Padding(0);
            contentSplitContainer.Name = "contentSplitContainer";
            // 
            // contentSplitContainer.Panel1
            // 
            contentSplitContainer.Panel1.Controls.Add(contentLeftPanel);
            // 
            // contentSplitContainer.Panel2
            // 
            contentSplitContainer.Panel2.Controls.Add(contentRightPanel);
            contentSplitContainer.Size = new Size(1280, 649);
            contentSplitContainer.SplitterDistance = 449;
            contentSplitContainer.SplitterWidth = 1;
            contentSplitContainer.TabIndex = 0;
            // 
            // contentLeftPanel
            // 
            contentLeftPanel.BackColor = Color.LightGray;
            contentLeftPanel.Controls.Add(subSplitLeftContainer);
            contentLeftPanel.Dock = DockStyle.Fill;
            contentLeftPanel.Location = new Point(0, 0);
            contentLeftPanel.Margin = new Padding(0);
            contentLeftPanel.Name = "contentLeftPanel";
            contentLeftPanel.Size = new Size(449, 649);
            contentLeftPanel.TabIndex = 0;
            // 
            // subSplitLeftContainer
            // 
            subSplitLeftContainer.Dock = DockStyle.Fill;
            subSplitLeftContainer.FixedPanel = FixedPanel.Panel1;
            subSplitLeftContainer.IsSplitterFixed = true;
            subSplitLeftContainer.Location = new Point(0, 0);
            subSplitLeftContainer.Margin = new Padding(0);
            subSplitLeftContainer.Name = "subSplitLeftContainer";
            subSplitLeftContainer.Orientation = Orientation.Horizontal;
            // 
            // subSplitLeftContainer.Panel1
            // 
            subSplitLeftContainer.Panel1.Controls.Add(headLeftLabel);
            // 
            // subSplitLeftContainer.Panel2
            // 
            subSplitLeftContainer.Panel2.Controls.Add(subSubSplitLeftContainer);
            subSplitLeftContainer.Size = new Size(449, 649);
            subSplitLeftContainer.SplitterWidth = 1;
            subSplitLeftContainer.TabIndex = 0;
            // 
            // headLeftLabel
            // 
            headLeftLabel.Dock = DockStyle.Fill;
            headLeftLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            headLeftLabel.Location = new Point(0, 0);
            headLeftLabel.Margin = new Padding(0);
            headLeftLabel.Name = "headLeftLabel";
            headLeftLabel.Size = new Size(449, 50);
            headLeftLabel.TabIndex = 0;
            headLeftLabel.Text = "Detail";
            headLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // subSubSplitLeftContainer
            // 
            subSubSplitLeftContainer.BackColor = Color.Transparent;
            subSubSplitLeftContainer.Dock = DockStyle.Fill;
            subSubSplitLeftContainer.FixedPanel = FixedPanel.Panel1;
            subSubSplitLeftContainer.IsSplitterFixed = true;
            subSubSplitLeftContainer.Location = new Point(0, 0);
            subSubSplitLeftContainer.Margin = new Padding(0);
            subSubSplitLeftContainer.Name = "subSubSplitLeftContainer";
            subSubSplitLeftContainer.Orientation = Orientation.Horizontal;
            // 
            // subSubSplitLeftContainer.Panel1
            // 
            subSubSplitLeftContainer.Panel1.Controls.Add(imageScreenControlNew);
            subSubSplitLeftContainer.Panel1.Padding = new Padding(45, 0, 45, 0);
            subSubSplitLeftContainer.Size = new Size(449, 598);
            subSubSplitLeftContainer.SplitterDistance = 249;
            subSubSplitLeftContainer.SplitterWidth = 1;
            subSubSplitLeftContainer.TabIndex = 0;
            // 
            // imageScreenControlNew
            // 
            imageScreenControlNew.BackColor = Color.Transparent;
            imageScreenControlNew.DisplayImage = null;
            imageScreenControlNew.Dock = DockStyle.Fill;
            imageScreenControlNew.Location = new Point(45, 0);
            imageScreenControlNew.Name = "imageScreenControlNew";
            imageScreenControlNew.Size = new Size(359, 249);
            imageScreenControlNew.TabIndex = 0;
            // 
            // contentRightPanel
            // 
            contentRightPanel.BackColor = Color.White;
            contentRightPanel.Controls.Add(subSplitRightContainer);
            contentRightPanel.Dock = DockStyle.Fill;
            contentRightPanel.Location = new Point(0, 0);
            contentRightPanel.Margin = new Padding(0);
            contentRightPanel.Name = "contentRightPanel";
            contentRightPanel.Size = new Size(830, 649);
            contentRightPanel.TabIndex = 0;
            // 
            // subSplitRightContainer
            // 
            subSplitRightContainer.Dock = DockStyle.Fill;
            subSplitRightContainer.FixedPanel = FixedPanel.Panel1;
            subSplitRightContainer.IsSplitterFixed = true;
            subSplitRightContainer.Location = new Point(0, 0);
            subSplitRightContainer.Margin = new Padding(0);
            subSplitRightContainer.Name = "subSplitRightContainer";
            subSplitRightContainer.Orientation = Orientation.Horizontal;
            // 
            // subSplitRightContainer.Panel1
            // 
            subSplitRightContainer.Panel1.Controls.Add(rightSectionHeaderControl);
            subSplitRightContainer.Size = new Size(830, 649);
            subSplitRightContainer.SplitterWidth = 1;
            subSplitRightContainer.TabIndex = 0;
            // 
            // rightSectionHeaderControl
            // 
            rightSectionHeaderControl.BackColor = Color.Transparent;
            rightSectionHeaderControl.Dock = DockStyle.Top;
            rightSectionHeaderControl.HeaderText = "";
            rightSectionHeaderControl.Location = new Point(0, 0);
            rightSectionHeaderControl.Margin = new Padding(0);
            rightSectionHeaderControl.Name = "rightSectionHeaderControl";
            rightSectionHeaderControl.Size = new Size(830, 50);
            rightSectionHeaderControl.TabIndex = 0;
            // 
            // AddPostPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(mainSplitContainer);
            Margin = new Padding(0);
            Name = "AddPostPage";
            Size = new Size(1280, 720);
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            contentSplitContainer.Panel1.ResumeLayout(false);
            contentSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).EndInit();
            contentSplitContainer.ResumeLayout(false);
            contentLeftPanel.ResumeLayout(false);
            subSplitLeftContainer.Panel1.ResumeLayout(false);
            subSplitLeftContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)subSplitLeftContainer).EndInit();
            subSplitLeftContainer.ResumeLayout(false);
            subSubSplitLeftContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)subSubSplitLeftContainer).EndInit();
            subSubSplitLeftContainer.ResumeLayout(false);
            contentRightPanel.ResumeLayout(false);
            subSplitRightContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)subSplitRightContainer).EndInit();
            subSplitRightContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer mainSplitContainer;
        private NavBarControl navBarControl;
        private SplitContainer contentSplitContainer;
        private Panel contentLeftPanel;
        private Panel contentRightPanel;
        private SplitContainer subSplitLeftContainer;
        private SplitContainer subSplitRightContainer;
        private SectionHeaderControl rightSectionHeaderControl;
        private Label headLeftLabel;
        private SplitContainer subSubSplitLeftContainer;
        private UserControlComponents.ImageScreenControlNew imageScreenControlNew;
    }
}
