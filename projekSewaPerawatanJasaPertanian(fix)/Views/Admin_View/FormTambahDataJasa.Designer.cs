namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{
    partial class FormTambahDataJasa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTambahDataJasa));
            this.txtIconJasa = new System.Windows.Forms.TextBox();
            this.txtNamaJasa = new System.Windows.Forms.TextBox();
            this.txtDeskripsiJasa = new System.Windows.Forms.TextBox();
            this.txtHarga = new System.Windows.Forms.TextBox();
            this.btnTambahJasa = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIconJasa
            // 
            this.txtIconJasa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIconJasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIconJasa.Location = new System.Drawing.Point(50, 156);
            this.txtIconJasa.Name = "txtIconJasa";
            this.txtIconJasa.Size = new System.Drawing.Size(323, 19);
            this.txtIconJasa.TabIndex = 2;
            // 
            // txtNamaJasa
            // 
            this.txtNamaJasa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNamaJasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaJasa.Location = new System.Drawing.Point(439, 156);
            this.txtNamaJasa.Name = "txtNamaJasa";
            this.txtNamaJasa.Size = new System.Drawing.Size(323, 19);
            this.txtNamaJasa.TabIndex = 3;
            // 
            // txtDeskripsiJasa
            // 
            this.txtDeskripsiJasa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeskripsiJasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeskripsiJasa.Location = new System.Drawing.Point(55, 262);
            this.txtDeskripsiJasa.Name = "txtDeskripsiJasa";
            this.txtDeskripsiJasa.Size = new System.Drawing.Size(707, 19);
            this.txtDeskripsiJasa.TabIndex = 5;
            // 
            // txtHarga
            // 
            this.txtHarga.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHarga.Location = new System.Drawing.Point(55, 444);
            this.txtHarga.Name = "txtHarga";
            this.txtHarga.Size = new System.Drawing.Size(302, 19);
            this.txtHarga.TabIndex = 6;
            // 
            // btnTambahJasa
            // 
            this.btnTambahJasa.BackColor = System.Drawing.Color.Transparent;
            this.btnTambahJasa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTambahJasa.BackgroundImage")));
            this.btnTambahJasa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTambahJasa.Location = new System.Drawing.Point(413, 530);
            this.btnTambahJasa.Name = "btnTambahJasa";
            this.btnTambahJasa.Size = new System.Drawing.Size(377, 76);
            this.btnTambahJasa.TabIndex = 12;
            this.btnTambahJasa.UseVisualStyleBackColor = false;
            this.btnTambahJasa.Click += new System.EventHandler(this.btnTambahJasa_Click_1);
            // 
            // btnBatal
            // 
            this.btnBatal.BackColor = System.Drawing.Color.Transparent;
            this.btnBatal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBatal.BackgroundImage")));
            this.btnBatal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBatal.Location = new System.Drawing.Point(30, 530);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(377, 76);
            this.btnBatal.TabIndex = 13;
            this.btnBatal.UseVisualStyleBackColor = false;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click_1);
            // 
            // FormTambahDataJasa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(831, 644);
            this.Controls.Add(this.btnBatal);
            this.Controls.Add(this.btnTambahJasa);
            this.Controls.Add(this.txtHarga);
            this.Controls.Add(this.txtDeskripsiJasa);
            this.Controls.Add(this.txtNamaJasa);
            this.Controls.Add(this.txtIconJasa);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormTambahDataJasa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTambahLayanan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIconJasa;
        private System.Windows.Forms.TextBox txtNamaJasa;
        private System.Windows.Forms.TextBox txtDeskripsiJasa;
        private System.Windows.Forms.TextBox txtHarga;
        private System.Windows.Forms.Button btnTambahJasa;
        private System.Windows.Forms.Button btnBatal;
    }
}