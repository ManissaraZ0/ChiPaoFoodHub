namespace FoodHubCustomerApp.UserControlPages
{
    partial class HomePage
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
            mainSplitContainer = new SplitContainer();
            navBarControl = new NavBarControl();
            contentSplitContainer = new SplitContainer();
            sectionHeaderControl = new SectionHeaderControl();
            flowContentLayoutPanel = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).BeginInit();
            contentSplitContainer.Panel1.SuspendLayout();
            contentSplitContainer.Panel2.SuspendLayout();
            contentSplitContainer.SuspendLayout();
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
            mainSplitContainer.Panel1.BackColor = Color.Transparent;
            mainSplitContainer.Panel1.Controls.Add(navBarControl);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(contentSplitContainer);
            mainSplitContainer.Size = new Size(1280, 720);
            mainSplitContainer.SplitterDistance = 70;
            mainSplitContainer.TabIndex = 0;
            // 
            // navBarControl
            // 
            navBarControl.BackColor = Color.White;
            navBarControl.Dock = DockStyle.Fill;
            navBarControl.Location = new Point(0, 0);
            navBarControl.LogoImage = null;
            navBarControl.MaximumSize = new Size(0, 70);
            navBarControl.MinimumSize = new Size(0, 70);
            navBarControl.Name = "navBarControl";
            navBarControl.Size = new Size(1280, 70);
            navBarControl.TabIndex = 0;
            // 
            // contentSplitContainer
            // 
            contentSplitContainer.BackColor = Color.White;
            contentSplitContainer.Dock = DockStyle.Fill;
            contentSplitContainer.FixedPanel = FixedPanel.Panel1;
            contentSplitContainer.IsSplitterFixed = true;
            contentSplitContainer.Location = new Point(0, 0);
            contentSplitContainer.Margin = new Padding(0);
            contentSplitContainer.Name = "contentSplitContainer";
            contentSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // contentSplitContainer.Panel1
            // 
            contentSplitContainer.Panel1.BackColor = Color.Transparent;
            contentSplitContainer.Panel1.Controls.Add(sectionHeaderControl);
            // 
            // contentSplitContainer.Panel2
            // 
            contentSplitContainer.Panel2.BackColor = Color.Transparent;
            contentSplitContainer.Panel2.Controls.Add(flowContentLayoutPanel);
            contentSplitContainer.Size = new Size(1280, 646);
            contentSplitContainer.TabIndex = 0;
            // 
            // sectionHeaderControl
            // 
            sectionHeaderControl.BackColor = Color.White;
            sectionHeaderControl.Dock = DockStyle.Top;
            sectionHeaderControl.HeaderText = "Recommendation Restaurants";
            sectionHeaderControl.Location = new Point(0, 0);
            sectionHeaderControl.Name = "sectionHeaderControl";
            sectionHeaderControl.Size = new Size(1280, 50);
            sectionHeaderControl.TabIndex = 0;
            // 
            // flowContentLayoutPanel
            // 
            flowContentLayoutPanel.AutoScroll = true;
            flowContentLayoutPanel.BackColor = Color.White;
            flowContentLayoutPanel.Dock = DockStyle.Fill;
            flowContentLayoutPanel.Location = new Point(0, 0);
            flowContentLayoutPanel.Margin = new Padding(0);
            flowContentLayoutPanel.Name = "flowContentLayoutPanel";
            flowContentLayoutPanel.Size = new Size(1280, 592);
            flowContentLayoutPanel.TabIndex = 0;
            // 
            // HomePage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(mainSplitContainer);
            Margin = new Padding(0);
            Name = "HomePage";
            Size = new Size(1280, 720);
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            contentSplitContainer.Panel1.ResumeLayout(false);
            contentSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).EndInit();
            contentSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer mainSplitContainer;
        private SplitContainer contentSplitContainer;
        private NavBarControl navBarControl;
        private SectionHeaderControl sectionHeaderControl;
        private FlowLayoutPanel flowContentLayoutPanel;
    }
}
