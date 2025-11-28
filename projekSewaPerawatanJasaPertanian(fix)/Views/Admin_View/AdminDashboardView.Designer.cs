namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{
    partial class AdminDashboardView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminDashboardView));
            this.panelscroll = new System.Windows.Forms.Panel();
            this.panelcontent = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTransaksi = new System.Windows.Forms.Label();
            this.lblSelesai = new System.Windows.Forms.Label();
            this.lblTotalPendapatan = new System.Windows.Forms.Label();
            this.lblLayananAktif = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.linkRiwayatTransaksi = new System.Windows.Forms.LinkLabel();
            this.linkKelolaLayanan = new System.Windows.Forms.LinkLabel();
            this.panelscroll.SuspendLayout();
            this.panelcontent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelscroll
            // 
            this.panelscroll.AutoScroll = true;
            this.panelscroll.Controls.Add(this.panelcontent);
            this.panelscroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelscroll.Location = new System.Drawing.Point(0, 0);
            this.panelscroll.Name = "panelscroll";
            this.panelscroll.Size = new System.Drawing.Size(1904, 1061);
            this.panelscroll.TabIndex = 0;
            // 
            // panelcontent
            // 
            this.panelcontent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelcontent.BackgroundImage")));
            this.panelcontent.Controls.Add(this.linkKelolaLayanan);
            this.panelcontent.Controls.Add(this.linkRiwayatTransaksi);
            this.panelcontent.Controls.Add(this.button1);
            this.panelcontent.Controls.Add(this.flowLayoutPanel1);
            this.panelcontent.Controls.Add(this.tableLayoutPanel1);
            this.panelcontent.Location = new System.Drawing.Point(0, 3);
            this.panelcontent.Name = "panelcontent";
            this.panelcontent.Size = new System.Drawing.Size(1920, 1709);
            this.panelcontent.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.83519F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.7216F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.66592F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.88864F));
            this.tableLayoutPanel1.Controls.Add(this.lblTransaksi, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSelesai, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalPendapatan, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLayananAktif, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(146, 342);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1796, 61);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // lblTransaksi
            // 
            this.lblTransaksi.AutoSize = true;
            this.lblTransaksi.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransaksi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTransaksi.Location = new System.Drawing.Point(466, 0);
            this.lblTransaksi.Name = "lblTransaksi";
            this.lblTransaksi.Size = new System.Drawing.Size(46, 45);
            this.lblTransaksi.TabIndex = 5;
            this.lblTransaksi.Text = "0";
            // 
            // lblSelesai
            // 
            this.lblSelesai.AutoSize = true;
            this.lblSelesai.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelesai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblSelesai.Location = new System.Drawing.Point(1351, 0);
            this.lblSelesai.Name = "lblSelesai";
            this.lblSelesai.Size = new System.Drawing.Size(46, 45);
            this.lblSelesai.TabIndex = 4;
            this.lblSelesai.Text = "0";
            // 
            // lblTotalPendapatan
            // 
            this.lblTotalPendapatan.AutoSize = true;
            this.lblTotalPendapatan.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPendapatan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTotalPendapatan.Location = new System.Drawing.Point(3, 0);
            this.lblTotalPendapatan.Name = "lblTotalPendapatan";
            this.lblTotalPendapatan.Size = new System.Drawing.Size(46, 45);
            this.lblTotalPendapatan.TabIndex = 6;
            this.lblTotalPendapatan.Text = "0";
            // 
            // lblLayananAktif
            // 
            this.lblLayananAktif.AutoSize = true;
            this.lblLayananAktif.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayananAktif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblLayananAktif.Location = new System.Drawing.Point(909, 0);
            this.lblLayananAktif.Name = "lblLayananAktif";
            this.lblLayananAktif.Size = new System.Drawing.Size(46, 45);
            this.lblLayananAktif.TabIndex = 7;
            this.lblLayananAktif.Text = "0";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(25, 539);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1850, 1500);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(1666, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 69);
            this.button1.TabIndex = 4;
            this.button1.Text = "    ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkRiwayatTransaksi
            // 
            this.linkRiwayatTransaksi.ActiveLinkColor = System.Drawing.SystemColors.WindowText;
            this.linkRiwayatTransaksi.AutoSize = true;
            this.linkRiwayatTransaksi.BackColor = System.Drawing.Color.Transparent;
            this.linkRiwayatTransaksi.Font = new System.Drawing.Font("Verdana", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRiwayatTransaksi.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRiwayatTransaksi.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.linkRiwayatTransaksi.Location = new System.Drawing.Point(614, 177);
            this.linkRiwayatTransaksi.Name = "linkRiwayatTransaksi";
            this.linkRiwayatTransaksi.Size = new System.Drawing.Size(245, 35);
            this.linkRiwayatTransaksi.TabIndex = 15;
            this.linkRiwayatTransaksi.TabStop = true;
            this.linkRiwayatTransaksi.Text = "                       ";
            this.linkRiwayatTransaksi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRiwayatTransaksi_LinkClicked);
            // 
            // linkKelolaLayanan
            // 
            this.linkKelolaLayanan.ActiveLinkColor = System.Drawing.SystemColors.WindowText;
            this.linkKelolaLayanan.AutoSize = true;
            this.linkKelolaLayanan.BackColor = System.Drawing.Color.Transparent;
            this.linkKelolaLayanan.Font = new System.Drawing.Font("Verdana", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkKelolaLayanan.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkKelolaLayanan.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.linkKelolaLayanan.Location = new System.Drawing.Point(343, 177);
            this.linkKelolaLayanan.Name = "linkKelolaLayanan";
            this.linkKelolaLayanan.Size = new System.Drawing.Size(245, 35);
            this.linkKelolaLayanan.TabIndex = 16;
            this.linkKelolaLayanan.TabStop = true;
            this.linkKelolaLayanan.Text = "                       ";
            this.linkKelolaLayanan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkKelolaLayanan_LinkClicked);
            // 
            // AdminDashboardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1061);
            this.Controls.Add(this.panelscroll);
            this.Name = "AdminDashboardView";
            this.Text = "AdminDashboardView";
            this.panelscroll.ResumeLayout(false);
            this.panelcontent.ResumeLayout(false);
            this.panelcontent.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelscroll;
        private System.Windows.Forms.Panel panelcontent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTransaksi;
        private System.Windows.Forms.Label lblSelesai;
        private System.Windows.Forms.Label lblTotalPendapatan;
        private System.Windows.Forms.Label lblLayananAktif;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkKelolaLayanan;
        private System.Windows.Forms.LinkLabel linkRiwayatTransaksi;
    }
}