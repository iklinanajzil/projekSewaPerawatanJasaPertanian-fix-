//using projekSewaPerawatanJasaPertanian_fix_.Controllers;
//using projekSewaPerawatanJasaPertanian_fix_.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Windows.Forms;

//namespace projekSewaPerawatanJasaPertanian_fix_.Views
//{
//    public partial class FormCheckoutPembayaran : Form

//    {
//        private readonly DataServiceController _controller;
//        private readonly List<JasaModel> _jasa;
//        private readonly int _idTransaksi;

//        private string _pathBukti = "";

//        public FormCheckoutPembayaran(
//            List<JasaModel> jasa,
//            string penerima,
//            string hp,
//            string alamatLengkap,
//            int kec,
//            int kel,
//            int idTransaksi,
//            DataServiceController controller)
//        {
//            InitializeComponent();

//            _jasa = jasa;
//            _idTransaksi = idTransaksi;
//            _controller = controller;

//            lblNilaiNama.Text = penerima;
//            lblNilaiHP.Text = hp;
//            lblNilaiAlamat.Text = alamatLengkap + $"\nKec: {kec}, Kel: {kel}";
//        }

      
//        private void btnPilihBukti_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog();
//            ofd.Filter = "Gambar|*.jpg;*.jpeg;*.png|Semua File|*.*";

//            if (ofd.ShowDialog() == DialogResult.OK)
//            {
//                _pathBukti = ofd.FileName;
//                pcbPreview.ImageLocation = _pathBukti;
//                lblNamaFile.Text = Path.GetFileName(_pathBukti);
//            }
//        }

//        private void btnKirimBukti_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(_pathBukti))
//            {
//                MessageBox.Show("Silakan pilih bukti pembayaran terlebih dahulu.");
//                return;
//            }

//            try
//            {
//                byte[] buktiBytes = File.ReadAllBytes(_pathBukti);

//                bool sukses = _controller.UpdateBuktiPembayaran(_idTransaksi, buktiBytes);

//                if (sukses)
//                {
//                    MessageBox.Show("Bukti pembayaran berhasil dikirim!");
//                    this.Close();
//                }
//                else
//                {
//                    MessageBox.Show("Gagal mengirim bukti pembayaran.");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//        }
//    }
//}




//===============================================================================================================




using projekSewaPerawatanJasaPertanian_fix_.Controllers;
using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace projekSewaPerawatanJasaPertanian_fix_.Views
{

    using projekSewaPerawatanJasaPertanian_fix_.Models;
    using System;
    using System.Collections.Generic;

    
    public partial class FormCheckoutPembayaran : Form
    {
        private CheckoutModel _checkoutModel;
        private DataServiceController _controller = new DataServiceController();
        private List<JasaModel> _jasaDipilihList; 

        

        public FormCheckoutPembayaran(CheckoutModel model, List<JasaModel> jasaList)
        {
            InitializeComponent();
            _checkoutModel = model;
            _jasaDipilihList = jasaList; 
            LoadRingkasanPesanan();
        }

        private void LoadRingkasanPesanan()
        {
   
        }

        private void btnPilihFile_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFile = new OpenFileDialog();
        openFile.Filter = "File Gambar (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string sourcePath = openFile.FileName;


            _checkoutModel.BuktiPembayaranFilePath = sourcePath;

            MessageBox.Show($"File {Path.GetFileName(sourcePath)} berhasil dipilih.", "File Terpilih");
        }
    }

        private void btnSelesai_Click(object sender, EventArgs e)
        {

            try
            {
                
                int idTransaksiBaru = _controller.InsertPemesananLengkap(_checkoutModel, _jasaDipilihList);

                MessageBox.Show($"Bukti diunggah. Transaksi {idTransaksiBaru} berhasil dicatat.", "Upload Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan transaksi: {ex.Message}", "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Batalkan proses unggah bukti pembayaran? Pesanan akan hilang.", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
}




