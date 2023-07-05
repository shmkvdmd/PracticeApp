namespace PracticeApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel = new Panel();
            pictureBox = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // panel
            // 
            panel.AutoScroll = true;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Controls.Add(pictureBox);
            panel.Location = new Point(259, 12);
            panel.Name = "panel";
            panel.Size = new Size(938, 511);
            panel.TabIndex = 4;
            // 
            // pictureBox
            // 
            pictureBox.Image = Properties.Resources.WorldMap;
            pictureBox.Location = new Point(5, 37);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(928, 469);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 50);
            label1.Name = "label1";
            label1.Size = new Size(114, 23);
            label1.TabIndex = 5;
            label1.Text = "Температура";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 12);
            label2.Name = "label2";
            label2.Size = new Size(13, 15);
            label2.TabIndex = 6;
            label2.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(127, 12);
            label3.Name = "label3";
            label3.Size = new Size(13, 15);
            label3.TabIndex = 7;
            label3.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1203, 559);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel);
            Name = "Form1";
            Text = "Информ";
            panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel;
        private PictureBox pictureBox;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}