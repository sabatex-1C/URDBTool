namespace NTICS_KEY_MANAGER
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_edrpou = new System.Windows.Forms.TextBox();
            this.tb_Serial = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ЕДРПОУ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "SERIAL";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_edrpou
            // 
            this.tb_edrpou.Location = new System.Drawing.Point(133, 7);
            this.tb_edrpou.MaxLength = 8;
            this.tb_edrpou.Name = "tb_edrpou";
            this.tb_edrpou.Size = new System.Drawing.Size(60, 20);
            this.tb_edrpou.TabIndex = 3;
            this.tb_edrpou.Text = "12345678";
            // 
            // tb_Serial
            // 
            this.tb_Serial.Location = new System.Drawing.Point(133, 36);
            this.tb_Serial.Name = "tb_Serial";
            this.tb_Serial.Size = new System.Drawing.Size(657, 20);
            this.tb_Serial.TabIndex = 4;
            this.tb_Serial.Text = "8E7KMAW-C8SX2JW-9FXRBNW-UF1V95T-KWHJM5E-X17U3TT-79SDZJE-WGHE3WR-AJ25BGR-Y27GMRR-H" +
                "EWFKKR-HZTETRW";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 101);
            this.Controls.Add(this.tb_Serial);
            this.Controls.Add(this.tb_edrpou);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_edrpou;
        private System.Windows.Forms.TextBox tb_Serial;
    }
}

