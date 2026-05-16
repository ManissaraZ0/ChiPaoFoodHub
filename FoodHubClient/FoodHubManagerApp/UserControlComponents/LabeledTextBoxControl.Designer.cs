namespace FoodHubManagerApp.UserControlComponents
{
    partial class LabeledTextBoxControl
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
            label1 = new Label();
            panelBorder = new Panel();
            textBox1 = new TextBox();
            panelBorder.SuspendLayout();
            SuspendLayout();

            // label1
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Name = "Title";
            label1.Text = "";
            label1.Location = new Point(0, 0);
            label1.Padding = new Padding(0, 0, 0, 10);

            // textBox1
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Dock = DockStyle.Fill;
            textBox1.Font = new Font("Segoe UI", 10F);
            textBox1.Name = "textBox1";

            // panelBorder
            panelBorder.Controls.Add(textBox1);
            panelBorder.Name = "panelBorder";
            panelBorder.Padding = new Padding(10, 8, 10, 8);
            panelBorder.Paint += PanelBorder_Paint;
            panelBorder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelBorder.Location = new Point(0, 40);
            panelBorder.Size = new Size(364, 40);  // width จะขยายตาม Anchor
            panelBorder.Resize += (s, e) => panelBorder.Invalidate();

            // LabeledTextBoxControl
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelBorder);
            Controls.Add(label1);
            Name = "LabeledTextBoxControl";
            Size = new Size(364, 75);

            panelBorder.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panelBorder;
        private TextBox textBox1;
    }
}
