namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{
    partial class FormTambahJadwal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTambahJadwal));
            this.txtSlotKetersediaan = new System.Windows.Forms.TextBox();
            this.cmbNamaJasa = new System.Windows.Forms.ComboBox();
            this.txtHari = new System.Windows.Forms.TextBox();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.txtJamMulai = new System.Windows.Forms.TextBox();
            this.txtJamAkhir = new System.Windows.Forms.TextBox();
            this.btnTambahJadwal = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSlotKetersediaan
            // 
            this.txtSlotKetersediaan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlotKetersediaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlotKetersediaan.Location = new System.Drawing.Point(441, 368);
            this.txtSlotKetersediaan.Name = "txtSlotKetersediaan";
            this.txtSlotKetersediaan.Size = new System.Drawing.Size(323, 19);
            this.txtSlotKetersediaan.TabIndex = 4;
            // 
            // cmbNamaJasa
            // 
            this.cmbNamaJasa.FormattingEnabled = true;
            this.cmbNamaJasa.Location = new System.Drawing.Point(441, 154);
            this.cmbNamaJasa.Name = "cmbNamaJasa";
            this.cmbNamaJasa.Size = new System.Drawing.Size(334, 21);
            this.cmbNamaJasa.TabIndex = 5;
            // 
            // txtHari
            // 
            this.txtHari.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHari.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHari.Location = new System.Drawing.Point(49, 368);
            this.txtHari.Name = "txtHari";
            this.txtHari.Size = new System.Drawing.Size(323, 19);
            this.txtHari.TabIndex = 6;
            // 
            // dtpTanggal
            // 
            this.dtpTanggal.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTanggal.CustomFormat = "yyyy-MM-dd";
            this.dtpTanggal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTanggal.Location = new System.Drawing.Point(60, 154);
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Size = new System.Drawing.Size(298, 26);
            this.dtpTanggal.TabIndex = 11;
            this.dtpTanggal.Value = new System.DateTime(2025, 11, 27, 0, 0, 0, 0);
            // 
            // txtJamMulai
            // 
            this.txtJamMulai.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtJamMulai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJamMulai.Location = new System.Drawing.Point(49, 263);
            this.txtJamMulai.Name = "txtJamMulai";
            this.txtJamMulai.Size = new System.Drawing.Size(323, 19);
            this.txtJamMulai.TabIndex = 12;
            // 
            // txtJamAkhir
            // 
            this.txtJamAkhir.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtJamAkhir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJamAkhir.Location = new System.Drawing.Point(441, 263);
            this.txtJamAkhir.Name = "txtJamAkhir";
            this.txtJamAkhir.Size = new System.Drawing.Size(323, 19);
            this.txtJamAkhir.TabIndex = 13;
            // 
            // btnTambahJadwal
            // 
            this.btnTambahJadwal.BackColor = System.Drawing.Color.Transparent;
            this.btnTambahJadwal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTambahJadwal.BackgroundImage")));
            this.btnTambahJadwal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTambahJadwal.Location = new System.Drawing.Point(413, 447);
            this.btnTambahJadwal.Name = "btnTambahJadwal";
            this.btnTambahJadwal.Size = new System.Drawing.Size(377, 76);
            this.btnTambahJadwal.TabIndex = 14;
            this.btnTambahJadwal.UseVisualStyleBackColor = false;
            this.btnTambahJadwal.Click += new System.EventHandler(this.btnTambahJadwal_Click_1);
            // 
            // btnBatal
            // 
            this.btnBatal.BackColor = System.Drawing.Color.Transparent;
            this.btnBatal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBatal.BackgroundImage")));
            this.btnBatal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBatal.Location = new System.Drawing.Point(30, 447);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(377, 76);
            this.btnBatal.TabIndex = 15;
            this.btnBatal.UseVisualStyleBackColor = false;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // FormTambahJadwal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(831, 561);
            this.Controls.Add(this.btnBatal);
            this.Controls.Add(this.btnTambahJadwal);
            this.Controls.Add(this.txtJamAkhir);
            this.Controls.Add(this.txtJamMulai);
            this.Controls.Add(this.dtpTanggal);
            this.Controls.Add(this.txtHari);
            this.Controls.Add(this.cmbNamaJasa);
            this.Controls.Add(this.txtSlotKetersediaan);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormTambahJadwal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTambahJadwal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSlotKetersediaan;
        private System.Windows.Forms.ComboBox cmbNamaJasa;
        private System.Windows.Forms.TextBox txtHari;
        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.TextBox txtJamMulai;
        private System.Windows.Forms.TextBox txtJamAkhir;
        private System.Windows.Forms.Button btnTambahJadwal;
        private System.Windows.Forms.Button btnBatal;
    }
}