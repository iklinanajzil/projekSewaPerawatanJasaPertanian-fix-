using projekSewaPerawatanJasaPertanian_fix_.Controllers;
using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace projekSewaPerawatanJasaPertanian_fix_.Views
{
    public partial class FormCheckoutPembayaran : Form

    {
        private readonly DataServiceController _controller;
        private readonly List<JasaModel> _jasa;
        private readonly int _idTransaksi;

        private string _pathBukti = "";

        public FormCheckoutPembayaran(
            List<JasaModel> jasa,
            string penerima,
            string hp,
            string alamatLengkap,
            int kec,
            int kel,
            int idTransaksi,
            DataServiceController controller)
        {
            InitializeComponent();

            _jasa = jasa;
            _idTransaksi = idTransaksi;
            _controller = controller;

            lblNilaiNama.Text = penerima;
            lblNilaiHP.Text = hp;
            lblNilaiAlamat.Text = alamatLengkap + $"\nKec: {kec}, Kel: {kel}";
        }

      
        private void btnPilihBukti_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Gambar|*.jpg;*.jpeg;*.png|Semua File|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _pathBukti = ofd.FileName;
                pcbPreview.ImageLocation = _pathBukti;
                lblNamaFile.Text = Path.GetFileName(_pathBukti);
            }
        }

        private void btnKirimBukti_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathBukti))
            {
                MessageBox.Show("Silakan pilih bukti pembayaran terlebih dahulu.");
                return;
            }

            try
            {
                byte[] buktiBytes = File.ReadAllBytes(_pathBukti);

                bool sukses = _controller.UpdateBuktiPembayaran(_idTransaksi, buktiBytes);

                if (sukses)
                {
                    MessageBox.Show("Bukti pembayaran berhasil dikirim!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Gagal mengirim bukti pembayaran.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}



