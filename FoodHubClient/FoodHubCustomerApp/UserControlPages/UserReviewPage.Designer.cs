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
            mainSplitContainer = new SplitContainer();
            navBarControl = new NavBarControl();
            contentSplitContainer = new SplitContainer();
            leftContentFlowPanel = new FlowLayoutPanel();
            restaurantImageControl = new FoodHubCustomerApp.UserControlComponents.ImageScreenControlNew();
            headerPanel = new Panel();
            lblCategory = new Label();
            lblRestaurantName = new Label();
            lblRatingScore = new Label();
            ticketFlowPanel = new FlowLayoutPanel();
            lblRestaurantDescription = new Label();
            reviewFlowPanel = new FlowLayoutPanel();
            actionBottomPanel = new Panel();
            btnAddPost = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).BeginInit();
            contentSplitContainer.Panel1.SuspendLayout();
            contentSplitContainer.Panel2.SuspendLayout();
            contentSplitContainer.SuspendLayout();
            leftContentFlowPanel.SuspendLayout();
            headerPanel.SuspendLayout();
            actionBottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnAddPost).BeginInit();
            SuspendLayout();
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
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
            navBarControl.UserData = null;
            // 
            // contentSplitContainer
            // 
            contentSplitContainer.Dock = DockStyle.Fill;
            contentSplitContainer.IsSplitterFixed = true;
            contentSplitContainer.Location = new Point(0, 0);
            contentSplitContainer.Margin = new Padding(0);
            contentSplitContainer.Name = "contentSplitContainer";
            // 
            // contentSplitContainer.Panel1
            // 
            contentSplitContainer.Panel1.Controls.Add(leftContentFlowPanel);
            // 
            // contentSplitContainer.Panel2
            // 
            contentSplitContainer.Panel2.Controls.Add(reviewFlowPanel);
            contentSplitContainer.Panel2.Controls.Add(actionBottomPanel);
            contentSplitContainer.Size = new Size(1280, 646);
            contentSplitContainer.SplitterDistance = 896;
            contentSplitContainer.TabIndex = 0;
            // 
            // leftContentFlowPanel
            // 
            leftContentFlowPanel.AutoScroll = true;
            leftContentFlowPanel.Controls.Add(restaurantImageControl);
            leftContentFlowPanel.Controls.Add(headerPanel);
            leftContentFlowPanel.Controls.Add(lblRatingScore);
            leftContentFlowPanel.Controls.Add(ticketFlowPanel);
            leftContentFlowPanel.Controls.Add(lblRestaurantDescription);
            leftContentFlowPanel.Dock = DockStyle.Fill;
            leftContentFlowPanel.FlowDirection = FlowDirection.TopDown;
            leftContentFlowPanel.Location = new Point(0, 0);
            leftContentFlowPanel.Margin = new Padding(0);
            leftContentFlowPanel.Name = "leftContentFlowPanel";
            leftContentFlowPanel.Size = new Size(896, 646);
            leftContentFlowPanel.TabIndex = 0;
            leftContentFlowPanel.WrapContents = false;
            // 
            // restaurantImageControl
            // 
            restaurantImageControl.BackColor = Color.Transparent;
            restaurantImageControl.DisplayImage = null;
            restaurantImageControl.Location = new Point(0, 0);
            restaurantImageControl.Margin = new Padding(0, 0, 0, 10);
            restaurantImageControl.Name = "restaurantImageControl";
            restaurantImageControl.Size = new Size(896, 280);
            restaurantImageControl.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(lblCategory);
            headerPanel.Controls.Add(lblRestaurantName);
            headerPanel.Location = new Point(3, 293);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(896, 50);
            headerPanel.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCategory.ForeColor = Color.Gray;
            lblCategory.Location = new Point(194, 16);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(147, 25);
            lblCategory.TabIndex = 1;
            lblCategory.Text = "Category: Buffet";
            // 
            // lblRestaurantName
            // 
            lblRestaurantName.AutoSize = true;
            lblRestaurantName.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRestaurantName.Location = new Point(0, 0);
            lblRestaurantName.Margin = new Padding(0);
            lblRestaurantName.Name = "lblRestaurantName";
            lblRestaurantName.Size = new Size(191, 45);
            lblRestaurantName.TabIndex = 0;
            lblRestaurantName.Text = "Suki Teenoi";
            // 
            // lblRatingScore
            // 
            lblRatingScore.Location = new Point(0, 346);
            lblRatingScore.Margin = new Padding(0);
            lblRatingScore.Name = "lblRatingScore";
            lblRatingScore.Size = new Size(896, 30);
            lblRatingScore.TabIndex = 2;
            lblRatingScore.Text = "⭐ 4.85/5.00";
            lblRatingScore.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ticketFlowPanel
            // 
            ticketFlowPanel.Location = new Point(0, 376);
            ticketFlowPanel.Margin = new Padding(0);
            ticketFlowPanel.Name = "ticketFlowPanel";
            ticketFlowPanel.Size = new Size(896, 110);
            ticketFlowPanel.TabIndex = 3;
            // 
            // lblRestaurantDescription
            // 
            lblRestaurantDescription.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRestaurantDescription.Location = new Point(0, 486);
            lblRestaurantDescription.Margin = new Padding(0);
            lblRestaurantDescription.Name = "lblRestaurantDescription";
            lblRestaurantDescription.Size = new Size(896, 180);
            lblRestaurantDescription.TabIndex = 4;
            lblRestaurantDescription.Text = "Restaurant Description";
            // 
            // reviewFlowPanel
            // 
            reviewFlowPanel.AutoScroll = true;
            reviewFlowPanel.Dock = DockStyle.Fill;
            reviewFlowPanel.FlowDirection = FlowDirection.TopDown;
            reviewFlowPanel.Location = new Point(0, 0);
            reviewFlowPanel.Name = "reviewFlowPanel";
            reviewFlowPanel.Size = new Size(380, 546);
            reviewFlowPanel.TabIndex = 1;
            reviewFlowPanel.WrapContents = false;
            // 
            // actionBottomPanel
            // 
            actionBottomPanel.Controls.Add(btnAddPost);
            actionBottomPanel.Dock = DockStyle.Bottom;
            actionBottomPanel.Location = new Point(0, 546);
            actionBottomPanel.Margin = new Padding(0);
            actionBottomPanel.Name = "actionBottomPanel";
            actionBottomPanel.Size = new Size(380, 100);
            actionBottomPanel.TabIndex = 0;
            // 
            // btnAddPost
            // 
            btnAddPost.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAddPost.Cursor = Cursors.Hand;
            btnAddPost.Location = new Point(294, 35);
            btnAddPost.Name = "btnAddPost";
            btnAddPost.Size = new Size(40, 40);
            btnAddPost.SizeMode = PictureBoxSizeMode.Zoom;
            btnAddPost.TabIndex = 0;
            btnAddPost.TabStop = false;
            // 
            // UserReviewPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainSplitContainer);
            Margin = new Padding(0);
            Name = "UserReviewPage";
            Size = new Size(1280, 720);
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            contentSplitContainer.Panel1.ResumeLayout(false);
            contentSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).EndInit();
            contentSplitContainer.ResumeLayout(false);
            leftContentFlowPanel.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            actionBottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnAddPost).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer mainSplitContainer;
        private SplitContainer contentSplitContainer;
        private NavBarControl navBarControl;
        private FlowLayoutPanel leftContentFlowPanel;
        private UserControlComponents.ImageScreenControlNew restaurantImageControl;
        private Panel headerPanel;
        private Label lblCategory;
        private Label lblRestaurantName;
        private Label lblRatingScore;
        private FlowLayoutPanel ticketFlowPanel;
        private Label lblRestaurantDescription;
        private Panel actionBottomPanel;
        private PictureBox btnAddPost;
        private FlowLayoutPanel reviewFlowPanel;
    }
}
