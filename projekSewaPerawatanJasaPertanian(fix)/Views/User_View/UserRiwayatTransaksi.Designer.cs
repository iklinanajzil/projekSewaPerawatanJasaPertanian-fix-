namespace projekSewaPerawatanJasaPertanian_fix_.Views.User_View
{
    partial class UserRiwayatTransaksi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserRiwayatTransaksi));
            this.panelscroll = new System.Windows.Forms.Panel();
            this.panelcontent = new System.Windows.Forms.Panel();
            this.linkLayananTersedia = new System.Windows.Forms.LinkLabel();
            this.panelscroll.SuspendLayout();
            this.panelcontent.SuspendLayout();
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
            this.panelcontent.Controls.Add(this.linkLayananTersedia);
            this.panelcontent.Location = new System.Drawing.Point(3, 3);
            this.panelcontent.Name = "panelcontent";
            this.panelcontent.Size = new System.Drawing.Size(1967, 3031);
            this.panelcontent.TabIndex = 0;
            this.panelcontent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelcontent_Paint);
            // 
            // linkLayananTersedia
            // 
            this.linkLayananTersedia.AutoSize = true;
            this.linkLayananTersedia.BackColor = System.Drawing.Color.Transparent;
            this.linkLayananTersedia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLayananTersedia.LinkColor = System.Drawing.Color.Transparent;
            this.linkLayananTersedia.Location = new System.Drawing.Point(93, 389);
            this.linkLayananTersedia.Name = "linkLayananTersedia";
            this.linkLayananTersedia.Size = new System.Drawing.Size(185, 20);
            this.linkLayananTersedia.TabIndex = 0;
            this.linkLayananTersedia.TabStop = true;
            this.linkLayananTersedia.Text = "                                            ";
            this.linkLayananTersedia.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLayananTersedia_LinkClicked);
            // 
            // UserRiwayatTransaksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1061);
            this.Controls.Add(this.panelscroll);
            this.Name = "UserRiwayatTransaksi";
            this.Text = "UserRiwayatTransaksi";
            this.panelscroll.ResumeLayout(false);
            this.panelcontent.ResumeLayout(false);
            this.panelcontent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelscroll;
        private System.Windows.Forms.Panel panelcontent;
        private System.Windows.Forms.LinkLabel linkLayananTersedia;
    }
}