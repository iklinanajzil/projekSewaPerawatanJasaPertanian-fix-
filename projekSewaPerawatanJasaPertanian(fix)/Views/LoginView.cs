using projekSewaPerawatanJasaPertanian_fix_.Controllers;
using projekSewaPerawatanJasaPertanian_fix_.Helpers;
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
using projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View;
using projekSewaPerawatanJasaPertanian_fix_.Views.User_View;


namespace projekSewaPerawatanJasaPertanian_fix_.Views
{
    public partial class LoginView : Form
    {
        private AuthController authController;
        //private readonly IJasa productInterface;
        public LoginView()
        {
            InitializeComponent();
            authController = new AuthController();
            //    JasaInterface = new JasaController();
        }
        

        private void linkKeRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterView register = new RegisterView();
            register.Show();
            this.Hide();
        }

        private void linkRegister_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterView register = new RegisterView();
            register.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username & Password harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string hashPassword = PasswordHelper.HashPassword(password);
                UserModel user = new UserModel
                {
                    Username = username,
                    Password = hashPassword
                };

                var auth = authController.Login(user);
                if (auth != null)
                {
                    MessageBox.Show($"Login Berhasil. Selamat datang {user.Username}", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AppSession.SetUser(auth);

                    if (AppSession.CurrentUser.Role == UserRole.admin)
                    {
                        AdminDashboardView admin = new AdminDashboardView();
                        admin.FormClosed += (s, args) => this.Close();
                        admin.Show();
                        this.Hide();
                    }
                    else
                    {
                        UserDashboardView userView = new UserDashboardView();
                        userView.FormClosed += (s, args) => this.Close();
                        userView.Show();
                        this.Hide();
                        //    UserDashboardView userDashboardView = new UserDashboardView();
                        //    userDashboardView.Show();
                        //    this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Username atau Password salah. Silahkan Coba Lagi!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal Login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
