namespace FoodHubManagerApp.UserControlComponents
{
    partial class LabeledDateTimePicker
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            panelBorder = new System.Windows.Forms.Panel();
            textBox1 = new System.Windows.Forms.TextBox();
            panelBorder.SuspendLayout();
            SuspendLayout();

            // label1
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            label1.Name = "label1";
            label1.Text = "Expire Date";
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);

            // textBox1
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true; // ห้ามพิมพ์มือ
            textBox1.BackColor = System.Drawing.Color.White; // บังคับให้เป็นสีขาวแม้จะ ReadOnly
            textBox1.Cursor = System.Windows.Forms.Cursors.Hand; // เป็นรูปมือ

            // panelBorder
            panelBorder.Controls.Add(textBox1);
            panelBorder.Name = "panelBorder";
            panelBorder.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            panelBorder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelBorder.Location = new System.Drawing.Point(0, 40);
            panelBorder.Size = new System.Drawing.Size(364, 40);
            panelBorder.Cursor = System.Windows.Forms.Cursors.Hand; // เป็นรูปมือ

            // LabeledDateTimePicker
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelBorder);
            Controls.Add(label1);
            Name = "LabeledDateTimePicker";
            Size = new System.Drawing.Size(364, 75);

            panelBorder.ResumeLayout(false);
            panelBorder.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBorder;
        private System.Windows.Forms.TextBox textBox1;
    }
}