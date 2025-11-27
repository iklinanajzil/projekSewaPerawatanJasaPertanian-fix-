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

namespace projekSewaPerawatanJasaPertanian_fix_.Views
{
    public partial class RegisterView : Form
    {
        private AuthController controller;
        public RegisterView()
        {
            InitializeComponent();
            controller = new AuthController();
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                string username = tbUsername.Text;

                string password = PasswordHelper.HashPassword(tbPassword.Text);

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Semua field harus diisi!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    UserModel userRegister = new UserModel
                    {

                        Username = username,
                        Email = email,
                        Password = password,
                        Role = UserRole.pelanggan
                    };
                    var success = controller.Register(userRegister);
                    if (success)
                    {
                        MessageBox.Show("Registrasi berhasil", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginView loginView = new LoginView();
                        loginView.FormClosed += (s, args) => this.Close();
                        this.Hide();
                        loginView.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal melakukan pendaftaran: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Hide();
        }
    }
}
