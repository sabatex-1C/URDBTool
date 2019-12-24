namespace URDBTool
{
    partial class main_form
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
            this.btReindex = new System.Windows.Forms.Button();
            this.btFlashExcange = new System.Windows.Forms.Button();
            this.btFTPChange = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btReindex
            // 
            this.btReindex.Location = new System.Drawing.Point(12, 12);
            this.btReindex.Name = "btReindex";
            this.btReindex.Size = new System.Drawing.Size(147, 23);
            this.btReindex.TabIndex = 1;
            this.btReindex.Text = "Переіндексація бази";
            this.btReindex.UseVisualStyleBackColor = true;
            this.btReindex.Click += new System.EventHandler(this.btReindex_Click);
            // 
            // btFlashExcange
            // 
            this.btFlashExcange.Location = new System.Drawing.Point(12, 41);
            this.btFlashExcange.Name = "btFlashExcange";
            this.btFlashExcange.Size = new System.Drawing.Size(147, 23);
            this.btFlashExcange.TabIndex = 2;
            this.btFlashExcange.Text = "Обмін через флеш";
            this.btFlashExcange.UseVisualStyleBackColor = true;
            this.btFlashExcange.Click += new System.EventHandler(this.btFlashExcange_Click);
            // 
            // btFTPChange
            // 
            this.btFTPChange.Location = new System.Drawing.Point(12, 70);
            this.btFTPChange.Name = "btFTPChange";
            this.btFTPChange.Size = new System.Drawing.Size(147, 23);
            this.btFTPChange.TabIndex = 3;
            this.btFTPChange.Text = "Обмін через ФТП";
            this.btFTPChange.UseVisualStyleBackColor = true;
            this.btFTPChange.Click += new System.EventHandler(this.btFTPChange_Click);
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(182, 14);
            this.Status.Multiline = true;
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(422, 191);
            this.Status.TabIndex = 4;
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 232);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.btFTPChange);
            this.Controls.Add(this.btFlashExcange);
            this.Controls.Add(this.btReindex);
            this.Name = "main_form";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.main_form_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btReindex;
        private System.Windows.Forms.Button btFlashExcange;
        private System.Windows.Forms.Button btFTPChange;
        private System.Windows.Forms.TextBox Status;
    }
}

