using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{
    public partial class AdminDashboardView : Form
    {
        public AdminDashboardView()
        {
            InitializeComponent();
            //    this.Load += AdminDashboardView_Load;
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Hide();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Hide();
        }

        private void linkKelolaLayanan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminKelolaLayananView adminKelolaLayananView = new AdminKelolaLayananView();
            adminKelolaLayananView.Show();
            this.Hide();
        }

        private void linkRiwayatTransaksi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminRiwayatTransaksiView admin = new AdminRiwayatTransaksiView();
            admin.Show();
            this.Hide();
        }
    }
}
