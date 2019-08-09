namespace ExportWordFileFromTemplate
{
    partial class ExportFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportFile));
            this.btnChooseTemplate = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btThemFile = new System.Windows.Forms.Button();
            this.txtFileMau = new System.Windows.Forms.TextBox();
            this.txtnewFile = new System.Windows.Forms.TextBox();
            this.chbOpenFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnChooseTemplate
            // 
            this.btnChooseTemplate.Location = new System.Drawing.Point(76, 89);
            this.btnChooseTemplate.Name = "btnChooseTemplate";
            this.btnChooseTemplate.Size = new System.Drawing.Size(142, 40);
            this.btnChooseTemplate.TabIndex = 1;
            this.btnChooseTemplate.Text = "Chọn file mẫu";
            this.btnChooseTemplate.UseVisualStyleBackColor = true;
            this.btnChooseTemplate.Click += new System.EventHandler(this.btnChooseTemplate_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(76, 168);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(142, 40);
            this.btnSaveFile.TabIndex = 3;
            this.btnSaveFile.Text = "Lưu file ở";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btThemFile
            // 
            this.btThemFile.Location = new System.Drawing.Point(318, 293);
            this.btThemFile.Name = "btThemFile";
            this.btThemFile.Size = new System.Drawing.Size(75, 37);
            this.btThemFile.TabIndex = 4;
            this.btThemFile.Text = "OK";
            this.btThemFile.UseVisualStyleBackColor = true;
            this.btThemFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFileMau
            // 
            this.txtFileMau.Enabled = false;
            this.txtFileMau.Location = new System.Drawing.Point(283, 102);
            this.txtFileMau.Name = "txtFileMau";
            this.txtFileMau.Size = new System.Drawing.Size(314, 26);
            this.txtFileMau.TabIndex = 5;
            // 
            // txtnewFile
            // 
            this.txtnewFile.Enabled = false;
            this.txtnewFile.Location = new System.Drawing.Point(283, 175);
            this.txtnewFile.Name = "txtnewFile";
            this.txtnewFile.Size = new System.Drawing.Size(314, 26);
            this.txtnewFile.TabIndex = 6;
            // 
            // chbOpenFile
            // 
            this.chbOpenFile.AutoSize = true;
            this.chbOpenFile.Location = new System.Drawing.Point(283, 236);
            this.chbOpenFile.Name = "chbOpenFile";
            this.chbOpenFile.Size = new System.Drawing.Size(160, 24);
            this.chbOpenFile.TabIndex = 7;
            this.chbOpenFile.Text = "Mở sau khi lưu file";
            this.chbOpenFile.UseVisualStyleBackColor = true;
            // 
            // ExportFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chbOpenFile);
            this.Controls.Add(this.txtnewFile);
            this.Controls.Add(this.txtFileMau);
            this.Controls.Add(this.btThemFile);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnChooseTemplate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportFile";
            this.Text = "ExportFile";
            this.Load += new System.EventHandler(this.ExportFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChooseTemplate;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btThemFile;
        private System.Windows.Forms.TextBox txtFileMau;
        private System.Windows.Forms.TextBox txtnewFile;
        private System.Windows.Forms.CheckBox chbOpenFile;
    }
}