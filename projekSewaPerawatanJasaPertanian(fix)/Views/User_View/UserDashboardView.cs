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
        private readonly DataServiceController _dataService = new DataServiceController();

        public UserDashboardView()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.UserDashboardView_Load);
        }
  
        private void UserDashboardView_Load(object sender, EventArgs e)
        {
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


        private UserDashboardController controller = new UserDashboardController();
        private int currentUserId;  

        private void LoadDashboard()
        {
            MessageBox.Show("Load Jasa Cards dipanggil!");
            var stats = controller.GetDashboardStats(currentUserId);

            lblTotalPesanan.Text = stats.total.ToString();
            lblSedangBerlangsung.Text = stats.proses.ToString();
            lblSelesai.Text = stats.selesai.ToString();
            lblTotalPengeluaran.Text = "Rp " + stats.pengeluaran.ToString("N0");
        }

        private void btnPesanSekarang_Click(object sender, EventArgs e)
        {
            
        }

        private void linkRiwayatTransaksi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserRiwayatTransaksi userRiwayatTransaksi = new UserRiwayatTransaksi();
            userRiwayatTransaksi.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Hide();
        }

        private void panelcontent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPesanSekarangg_Click(object sender, EventArgs e)
        {
            List<int> idJadwalTerpilih = new List<int>();

            foreach (JasaCardControl card in flowLayoutPanelLayanan.Controls.OfType<JasaCardControl>())
            {
                if (card.IsSelected && card.Tag is int idJadwal)
                {
                    idJadwalTerpilih.Add(idJadwal);
                }
            }

            if (idJadwalTerpilih.Count > 0)
            {
                int idPenggunaSaatIni = 1;
                string namaPenerima = "Nama Pelanggan Contoh";
                string noHp = "081234567890";
                string alamatLengkap = "Jl. Contoh No. 1";
                int idKelurahan = 1;
                int idKecamatan = 1;
                string metodePembayaran = "Transfer Bank";

                try
                {
                    DataServiceController dataService = new DataServiceController();
                    dataService.InsertTransaksi(
                        idPenggunaSaatIni, idJadwalTerpilih,
                        namaPenerima, noHp, alamatLengkap,
                        idKelurahan, idKecamatan, metodePembayaran
                    );

                    MessageBox.Show("Pesanan berhasil dibuat! Silakan lakukan pembayaran.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadJasaCards(); 
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
    }
}


