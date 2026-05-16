namespace FoodHubManagerApp
{
    partial class ManagerApp
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
            timer1 = new System.Windows.Forms.Timer();

            SuspendLayout();
            // 
            // ManagerApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            MinimumSize = new Size(1280, 720);
            Name = "ManagerApp";
            Text = "Manager App";
            Load += ManagerApp_Load;
            ResumeLayout(false);

            //timer1
            timer1.Interval = 5000; // 5 วินาที
            timer1.Tick += timer1_Tick;
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
    }
}