using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View;
using projekSewaPerawatanJasaPertanian_fix_.Views.User_View;

namespace projekSewaPerawatanJasaPertanian_fix_.Views.User_View
{
    public partial class UserRiwayatTransaksi : Form
    {
        public UserRiwayatTransaksi()
        {
            InitializeComponent();
        }

        private void panelcontent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLayananTersedia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserDashboardView userDashboardView = new UserDashboardView();
            userDashboardView.Show();
            this.Hide();    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Hide();
        }
    }
}
