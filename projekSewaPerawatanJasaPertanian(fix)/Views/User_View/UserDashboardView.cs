// FILE: Views/User_View/UserDashboardView.cs

using projekSewaPerawatanJasaPertanian_fix_.Controllers;
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
using projekSewaPerawatanJasaPertanian_fix_.Views.User_View;

namespace projekSewaPerawatanJasaPertanian_fix_.Views.User_View
{
    // Asumsi: SessionManager dan properti lain tetap sama dari kode sebelumnya
    
    public partial class UserDashboardView : Form
    {
        private readonly DataServiceController _dataService = new DataServiceController();
        private UserDashboardController controller = new UserDashboardController();
        private int currentUserId;
        private int _idPengguna;
        private int _idPenggunaSaatIni;

        public UserDashboardView(int userId)
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.UserDashboardView_Load);
            _idPengguna = _idPengguna;
            _idPenggunaSaatIni = userId;
        }

        public UserDashboardView()
        {
        }

        public static class SessionManager
        {
            public static int CurrentUserId { get; set; } = 1;
        }


        private void UserDashboardView_Load(object sender, EventArgs e)
        {
            if (SessionManager.CurrentUserId > 0)
            {
                currentUserId = SessionManager.CurrentUserId;
                LoadDashboard();
            }
            LoadJasaCards();
        }

        private void LoadJasaCards()
        {
            this.flowLayoutPanelLayanan.Controls.Clear();

            try
            {
                List<JasaModel> jasaTersedia = _dataService.GetJasaTersedia();

                if (jasaTersedia != null && jasaTersedia.Any())
                {
                    foreach (var jasa in jasaTersedia)
                    {
                        JasaCardControl card = new JasaCardControl();
                        card.SetData(jasa);
                        flowLayoutPanelLayanan.Controls.Add(card);
                    }
                }
                else
                {
                    MessageBox.Show("Tidak ada layanan jasa yang tersedia saat ini.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat memuat data layanan: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDashboard()
        {
            var stats = controller.GetDashboardStats(currentUserId);
            // ... (logika LoadDashboard lainnya)
        }


        // File: UserDashboardView.cs

         // Properti ini harus tersedia dan berisi ID pengguna

        private void btnPesanSekarangg_Click(object sender, EventArgs e)
        {
            // 1. Ambil data jasa yang dipilih
            var jasaDipilihList = flowLayoutPanelLayanan.Controls
                .OfType<JasaCardControl>()
                .Where(card => card.IsSelected)
                .Select(card => card.JasaData)
                .ToList();

            // 2. Validasi (Hanya cek apakah minimal ada satu yang dipilih)
            if (jasaDipilihList.Count == 0)
            {
                MessageBox.Show("Pilih minimal satu jasa terlebih dahulu.");
                return;
            }

            // 3. Pastikan ID Pengguna tersedia
            if (_idPenggunaSaatIni <= 0)
            {
                MessageBox.Show("ID Pengguna tidak valid. Mohon login ulang.", "Error Sistem");
                return;
            }

            try
            {
                // 4. Panggil FormCheckout dengan parameter yang benar (List<JasaModel>, ID Pengguna)

                // CATATAN: Constructor FormCheckout HARUS diubah agar menerima List<JasaModel>
                FormCheckout form = new FormCheckout(jasaDipilihList, _idPenggunaSaatIni);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Pemesanan berhasil diproses!", "Sukses");
                    // Logika Refresh
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat memulai pemesanan: {ex.Message}", "Error");
            }
        }

        private void linkRiwayatTransaksi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserRiwayatTransaksi userRiwayatTransaksi = new UserRiwayatTransaksi();
            userRiwayatTransaksi.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SessionManager.CurrentUserId = 0;
            LoginView login = new LoginView();
            login.Show();
            this.Hide();
        }
    }
}

