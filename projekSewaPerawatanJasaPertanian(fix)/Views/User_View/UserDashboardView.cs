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
    public partial class UserDashboardView : Form
    {
        //public UserDashboardView()
        //{
        //    InitializeComponent();
        //}
        private readonly DataServiceController _dataService = new DataServiceController();

        public UserDashboardView()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.UserDashboardView_Load);

            // Pastikan flowLayoutPanelLayanan sudah ada di Designer
        }

        // 🚨 TAMBAHKAN ATAU KOREKSI METODE INI 🚨
        private void UserDashboardView_Load(object sender, EventArgs e)
        {
            // Panggil metode untuk memuat card saat Form dimuat
            LoadJasaCards();
            // Contoh: LoadDashboardStats(idPengguna);
        }

        private void LoadJasaCards()
        {
            // PASTIKAN NAMA INI SAMA DENGAN FLOWLAYOUTPANEL DI DESIGNER
            flowLayoutPanelLayanan.Controls.Clear();

            try
            {
                // Memanggil data dari DataServiceController
                List<JasaModel> jasaTersedia = _dataService.GetJasaTersedia();

                if (jasaTersedia != null && jasaTersedia.Any())
                {
                    foreach (var jasa in jasaTersedia)
                    {
                        JasaCardControl card = new JasaCardControl();
                        card.SetData(jasa);

                        // Tambahkan Card ke FlowLayoutPanel
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
                // Ini akan menangkap error koneksi atau SQL (penting untuk debugging!)
                MessageBox.Show($"Error saat memuat data layanan: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private UserDashboardController controller = new UserDashboardController();
        private int currentUserId;

        //private void UserDashboardView_Load(object sender, EventArgs e)
        //{
        //    LoadDashboard();
        //    LoadJasaCards();
        //}

        private void LoadDashboard()
        {
            MessageBox.Show("Load Jasa Cards dipanggil!");
            var stats = controller.GetDashboardStats(currentUserId);

            lblTotalPesanan.Text = stats.total.ToString();
            lblSedangBerlangsung.Text = stats.proses.ToString();
            lblSelesai.Text = stats.selesai.ToString();
            lblTotalPengeluaran.Text = "Rp " + stats.pengeluaran.ToString("N0");
        }


        // --- Logika Menampilkan Cards di FlowLayoutPanel ---
        //private void LoadJasaCards()
        //{
        //    // flowLayoutPanelLayanan adalah FlowLayoutPanel di Designer Anda
        //    flowLayoutPanelLayanan.Controls.Clear();

        //    DataServiceController dataService = new DataServiceController();
        //    List<JasaModel> jasaTersedia = dataService.GetJasaTersedia();

        //    foreach (var jasa in jasaTersedia)
        //    {
        //        JasaCardControl card = new JasaCardControl();
        //        card.SetData(jasa);
        //        flowLayoutPanelLayanan.Controls.Add(card);
        //    }
        //}
     
        private void linkKeRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Hide();
        }

        private void btnPesanSekarang_Click(object sender, EventArgs e)
        {
            List<int> idJadwalTerpilih = new List<int>();

            // Kumpulkan ID jadwal yang dicentang
            foreach (JasaCardControl card in flowLayoutPanelLayanan.Controls.OfType<JasaCardControl>())
            {
                if (card.IsSelected && card.Tag is int idJadwal)
                {
                    idJadwalTerpilih.Add(idJadwal);
                }
            }

            if (idJadwalTerpilih.Count > 0)
            {
                // --- DATA ASUMSI DARI SESI/FORM INPUT ALAMAT ---
                // Data ini HARUS didapatkan dari input user/sesi login
                int idPenggunaSaatIni = 1;
                string namaPenerima = "Nama Pelanggan Contoh";
                string noHp = "081234567890";
                string alamatLengkap = "Jl. Contoh No. 1";
                int idKelurahan = 1;
                int idKecamatan = 1;
                string metodePembayaran = "Transfer Bank";
                // ---------------------------------------------

                try
                {
                    DataServiceController dataService = new DataServiceController();
                    dataService.InsertTransaksi(
                        idPenggunaSaatIni, idJadwalTerpilih,
                        namaPenerima, noHp, alamatLengkap,
                        idKelurahan, idKecamatan, metodePembayaran
                    );

                    MessageBox.Show("Pesanan berhasil dibuat! Silakan lakukan pembayaran.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadJasaCards(); // Refresh daftar jasa
                                     // tabControlMain.SelectedTab = tabRiwayatTransaksi; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat memproses pesanan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih minimal satu layanan sebelum memesan.");
            }
        }

        private void linkRiwayatTransaksi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserRiwayatTransaksi userRiwayatTransaksi = new UserRiwayatTransaksi();
            userRiwayatTransaksi.Show();
            this.Hide();
        }  
    }
}


