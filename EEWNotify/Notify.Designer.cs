namespace EEWNotify
{
    partial class EEW_Window
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
            this.DataNotOREEW_c = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.shindo_c = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.region_name_c = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.magnitude_c = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.depth_c = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DataNotOREEW_c
            // 
            this.DataNotOREEW_c.AutoSize = true;
            this.DataNotOREEW_c.ForeColor = System.Drawing.Color.Black;
            this.DataNotOREEW_c.Location = new System.Drawing.Point(12, 9);
            this.DataNotOREEW_c.Name = "DataNotOREEW_c";
            this.DataNotOREEW_c.Size = new System.Drawing.Size(174, 15);
            this.DataNotOREEW_c.TabIndex = 0;
            this.DataNotOREEW_c.Text = "起動中です。しばらくお待ち下さい。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "最大震度";
            // 
            // shindo_c
            // 
            this.shindo_c.Location = new System.Drawing.Point(73, 33);
            this.shindo_c.Name = "shindo_c";
            this.shindo_c.Size = new System.Drawing.Size(39, 23);
            this.shindo_c.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "震源";
            // 
            // region_name_c
            // 
            this.region_name_c.Location = new System.Drawing.Point(73, 68);
            this.region_name_c.Name = "region_name_c";
            this.region_name_c.Size = new System.Drawing.Size(257, 23);
            this.region_name_c.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "マグニチュード";
            // 
            // magnitude_c
            // 
            this.magnitude_c.Location = new System.Drawing.Point(73, 106);
            this.magnitude_c.Name = "magnitude_c";
            this.magnitude_c.Size = new System.Drawing.Size(49, 23);
            this.magnitude_c.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "震源の深さ";
            // 
            // depth_c
            // 
            this.depth_c.Location = new System.Drawing.Point(201, 33);
            this.depth_c.Name = "depth_c";
            this.depth_c.Size = new System.Drawing.Size(39, 23);
            this.depth_c.TabIndex = 8;
            // 
            // EEW_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 142);
            this.Controls.Add(this.depth_c);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.magnitude_c);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.region_name_c);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.shindo_c);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DataNotOREEW_c);
            this.Name = "EEW_Window";
            this.Text = "緊急地震速報ウィンドウ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_Closing);
            this.Load += new System.EventHandler(this.Window_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label DataNotOREEW_c;
        private Label label2;
        private TextBox shindo_c;
        private Label label3;
        private TextBox region_name_c;
        private Label label4;
        private TextBox magnitude_c;
        private Label label5;
        private TextBox depth_c;
    }
}