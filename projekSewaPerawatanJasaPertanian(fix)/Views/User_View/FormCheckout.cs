
//using Npgsql;
//using projekSewaPerawatanJasaPertanian_fix_.Controllers;
//using projekSewaPerawatanJasaPertanian_fix_.Database;
//using projekSewaPerawatanJasaPertanian_fix_.Models;
//using projekSewaPerawatanJasaPertanian_fix_.Views;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Windows.Forms;

//namespace projekSewaPerawatanJasaPertanian_fix_.Views
//{
//    public partial class FormCheckout : Form
//    {
//        private readonly DbContext _dbContext;
//        private readonly DataServiceController _controller;
//        private readonly int _idPengguna;

//        private readonly List<JasaModel> _jasaDipilih;
//        private decimal _totalHarga = 0;

//        public FormCheckout(List<JasaModel> jasaDipilih)
//        {
//            InitializeComponent();

//            _dbContext = new DbContext();
//            _controller = new DataServiceController();

//            _jasaDipilih = jasaDipilih;
//            //_idPengguna = userId;
//        }

//        private void FormCheckout_Load(object sender, EventArgs e)
//        {
//            decimal total = 0;

//            foreach (var jasa in _jasaDipilih)
//            {
//                lbJasa.Items.Add($"{jasa.NamaJasa} - Rp {jasa.HargaJasa:N0}");
//                total += jasa.HargaJasa;
//            }

//            lblTotalPembayaran.Text = $"Rp {total:N0}";
//            LoadKecamatan();
//        }

//        private void LoadKecamatan()
//        {
//            using (NpgsqlConnection conn = _dbContext.GetConnection())
//            {
//                conn.Open();
//                var cmd = new NpgsqlCommand(
//                    "SELECT id_kecamatan, nama_kecamatan FROM kecamatan ORDER BY nama_kecamatan",
//                    conn);

//                var reader = cmd.ExecuteReader();
//                Dictionary<int, string> data = new Dictionary<int, string>();

//                while (reader.Read())
//                    data.Add(reader.GetInt32(0), reader.GetString(1));

//                cmbKecamatan.DataSource = new BindingSource(data, null);
//                cmbKecamatan.DisplayMember = "Value";
//                cmbKecamatan.ValueMember = "Key";
//            }
//        }


//        private void btnBatal_Click(object sender, EventArgs e)
//        {
//            DialogResult konfirmasi = MessageBox.Show(
//            "Apakah Anda yakin ingin membatalkan pesanan ini?",
//            "Konfirmasi Pembatalan",
//            MessageBoxButtons.YesNo,
//            MessageBoxIcon.Question);

//            if (konfirmasi == DialogResult.Yes)
//            {
//                // 1. Set DialogResult ke Cancel (opsional, tetapi praktik yang baik)
//                this.DialogResult = DialogResult.Cancel;

//                // 2. Tutup Form Checkout
//                this.Close();
//            }
//        }

//        private void cmbKecamatan_SelectedIndexChanged_1(object sender, EventArgs e)
//        {
//            int idKec = ((KeyValuePair<int, string>)cmbKecamatan.SelectedItem).Key;

//            using (NpgsqlConnection conn = _dbContext.GetConnection())
//            {
//                conn.Open();
//                var cmd = new NpgsqlCommand(
//                    "SELECT id_kelurahan, nama_kelurahan FROM kelurahan WHERE id_kecamatan = @id ORDER BY nama_kelurahan",
//                    conn);

//                cmd.Parameters.AddWithValue("@id", idKec);

//                var reader = cmd.ExecuteReader();
//                Dictionary<int, string> data = new Dictionary<int, string>();

//                while (reader.Read())
//                    data.Add(reader.GetInt32(0), reader.GetString(1));

//                cmbKelurahan.DataSource = new BindingSource(data, null);
//                cmbKelurahan.DisplayMember = "Value";
//                cmbKelurahan.ValueMember = "Key";
//            }

//        }

//        private void btnLanjutPembayaran_Click_1(object sender, EventArgs e)
//        {
//            if (txtNamaPenerima.Text == "" || txtNomorHP.Text == "" || txtAlamatLengkap.Text == "")
//            {
//                MessageBox.Show("Lengkapi semua data penerima.");
//                return;
//            }

//            int idKec = ((KeyValuePair<int, string>)cmbKecamatan.SelectedItem).Key;
//            int idKel = ((KeyValuePair<int, string>)cmbKelurahan.SelectedItem).Key;

//            // -----------------------------
//            // INSERT TRANSAKSI DAN DAPATKAN ID
//            // -----------------------------
//            decimal totalHarga = 0;
//            foreach (var j in _jasaDipilih)
//            {
//                totalHarga += j.HargaJasa;
//            }

//            int idTransaksi = _controller.InsertTransaksi(
//                _idPengguna,
//                txtNamaPenerima.Text,
//                txtNomorHP.Text,
//                txtAlamatLengkap.Text,
//                idKec,
//                idKel,
//                "Transfer Bank",   // metode pembayaran contoh
//                totalHarga
//            );

//            if (idTransaksi <= 0)
//            {
//                MessageBox.Show("Gagal membuat transaksi!");
//                return;
//            }

//            // -----------------------------
//            // BUKA FORM PEMBAYARAN
//            // -----------------------------
//            FormCheckoutPembayaran f = new FormCheckoutPembayaran(
//                _jasaDipilih,
//                txtNamaPenerima.Text,
//                txtNomorHP.Text,
//                txtAlamatLengkap.Text,
//                idKec,
//                idKel,
//                idTransaksi,
//                _controller
//            );

//            f.Show();

//        }
//    }
//}



//============================================================================



// File: Forms/FormCheckout.cs

//using projekSewaPerawatanJasaPertanian_fix_.Controllers;
//using projekSewaPerawatanJasaPertanian_fix_.Models;
//using projekSewaPerawatanJasaPertanian_fix_.Views;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Globalization;
//using System.Linq;
//using System.Windows.Forms;
//// Pastikan tidak ada using Microsoft.EntityFrameworkCore; jika tidak dipakai

//namespace projekSewaPerawatanJasaPertanian_fix_.Views

//{

//    public partial class FormCheckout : Form
//    {
//        private List<JasaModel> _jasaDipilihList;
//        private DataServiceController _controller = new DataServiceController();
//        private int _idPenggunaSaatIni;
//        private decimal _totalHargaKeseluruhan; // FIELD BARU

//        // Constructor Menerima List<JasaModel>
//        public FormCheckout(List<JasaModel> jasaList, int idPengguna)
//        {
//            InitializeComponent();
//            _jasaDipilihList = jasaList;
//            _idPenggunaSaatIni = idPengguna;

//            // HITUNG TOTAL HARGA KESELURUHAN (Mengatasi Error TotalHarga)
//            _totalHargaKeseluruhan = jasaList.Sum(j => j.TotalHarga);

//            // Panggil LoadDataAwal
//            LoadDataAwal();
//        }


    
//        // --- A. Pemuatan ComboBox (Logika Binding Tetap Sama) ---

//        private void LoadKecamatan()
//    {
//        try
//        {
//            DataTable dtKecamatan = _controller.GetAllKecamatan();

//            cmbKecamatan.DataSource = dtKecamatan;
//            cmbKecamatan.DisplayMember = "nama_kecamatan";
//            cmbKecamatan.ValueMember = "id_kecamatan";

//            if (dtKecamatan.Rows.Count > 0)
//            {
//                cmbKecamatan.SelectedIndex = 0;
//                LoadKelurahan();
//            }
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Gagal memuat data Kecamatan: {ex.Message}", "Error Data Kecamatan");
//        }
//    }

//    private void cmbKecamatan_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        if (cmbKecamatan.SelectedIndex > -1 && cmbKecamatan.DataSource != null)
//        {
//            LoadKelurahan();
//        }
//    }

//    private void LoadKelurahan()
//    {
//        if (cmbKecamatan.SelectedValue == null || cmbKecamatan.SelectedValue is DataRowView)
//        {
//            cmbKelurahan.DataSource = null;
//            cmbKelurahan.Items.Clear();
//            return;
//        }

//        try
//        {
//            int idKecamatan = Convert.ToInt32(cmbKecamatan.SelectedValue);
//            var listKelurahan = _controller.GetKelurahanByKecamatan(idKecamatan);

//            cmbKelurahan.DataSource = listKelurahan;
//            cmbKelurahan.DisplayMember = "nama_kelurahan";
//            cmbKelurahan.ValueMember = "id_kelurahan";

//            if (listKelurahan.Count > 0)
//            {
//                cmbKelurahan.SelectedIndex = 0;
//            }
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Gagal memuat data Kelurahan: {ex.Message}", "Error Data Kelurahan");
//        }
//    }

//    private void LoadMetodePembayaran()
//    {
//        cmbMetodePembayaran.Items.Clear();
//        cmbMetodePembayaran.Items.Add("Tunai");
//        cmbMetodePembayaran.Items.Add("Bank BCA");
//        cmbMetodePembayaran.Items.Add("Bank BRI");
//        cmbMetodePembayaran.Items.Add("Dana");
//        cmbMetodePembayaran.SelectedIndex = 0;
//    }

//    // --- B. Logika Tombol Lanjut dan Batal ---

//    private void btnLanjutPembayaran_Click(object sender, EventArgs e)
//    {
//            string metodeTerpilih = cmbMetodePembayaran.Text;
//            string statusAwal = "diproses";

//            // UBAH: Model sekarang hanya menampung data Alamat, Penerima, Metode, dan Total Keseluruhan
//            CheckoutModel modelDasar = new CheckoutModel
//            {
//                IdPengguna = _idPenggunaSaatIni,
//                NamaPenerima = txtNamaPenerima.Text,
//                NomorHP = txtNomorHP.Text,
//                AlamatLengkap = txtAlamatLengkap.Text,

//                IdKecamatan = Convert.ToInt32(cmbKecamatan.SelectedValue),
//                IdKelurahan = Convert.ToInt32(cmbKelurahan.SelectedValue),

//                NamaKecamatan = cmbKecamatan.Text,
//                NamaKelurahan = cmbKelurahan.Text,

//                // UBAH: TotalHarga menggunakan total keseluruhan
//                TotalHarga = _totalHargaKeseluruhan,

//                MetodePembayaran = metodeTerpilih,
//                Status = statusAwal

//                // Hapus IdJasa dan IdJadwal dari sini, karena sekarang ada banyak!
//                // Kita akan meneruskan _jasaDipilihList ke Controller nanti.
//            };

//            try
//            {
//                if (modelDasar.MetodePembayaran == "Tunai")
//                {
//                    // 3A. Logika Tunai: Panggil Controller dengan DAFTAR JASA
//                    int idTransaksiBaru = _controller.InsertPemesananLengkap(modelDasar, _jasaDipilihList);
//                    MessageBox.Show($"Pemesanan Tunai berhasil dicatat. ID Transaksi Utama: {idTransaksiBaru}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    this.DialogResult = DialogResult.OK;
//                    this.Close();
//                }
//                else
//                {
//                    // 3B. Logika Non-Tunai: Lanjut ke Form Upload Bukti
//                    FormCheckoutPembayaran formUpload = new FormCheckoutPembayaran(modelDasar, _jasaDipilihList);
//                    if (formUpload.ShowDialog() == DialogResult.OK)
//                    {
//                        this.DialogResult = DialogResult.OK;
//                        this.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Gagal memproses pesanan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
   

//    private void btnBatal_Click(object sender, EventArgs e)
//    {
//        if (MessageBox.Show("Batalkan pesanan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//        {
//            this.DialogResult = DialogResult.Cancel;
//            this.Close();
//        }
//    }
//}
//}


// File: Forms/FormCheckout.cs

using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic; 
using System.Data;
using System.Linq; 
using System.Windows.Forms;
using System.Globalization; 
using projekSewaPerawatanJasaPertanian_fix_.Controllers;
using System.Collections.Generic;

namespace projekSewaPerawatanJasaPertanian_fix_.Views

{

    public partial class FormCheckout : Form
    {
        private List<JasaModel> _jasaDipilihList;
        
        private DataServiceController _controller = new DataServiceController();
        private int _idPenggunaSaatIni;
        private decimal _totalHargaKeseluruhan;

        public FormCheckout(List<JasaModel> jasaList, int idPengguna)
        {
            InitializeComponent();

            if (jasaList == null || jasaList.Count == 0)
            {
                throw new ArgumentException("Daftar jasa tidak boleh kosong.", nameof(jasaList));
            }

            _jasaDipilihList = jasaList;
            _idPenggunaSaatIni = idPengguna;

            _totalHargaKeseluruhan = jasaList.Sum(j => j.TotalHarga);

            LoadDataAwal();
        }


        private void LoadDataAwal()
        {
            LoadKecamatan();
            LoadMetodePembayaran();
            LoadRingkasanJadwal(); 
        }

        private void LoadRingkasanJadwal()
        {
            

            string ringkasanText = "";
            int counter = 1;

            foreach (var jasa in _jasaDipilihList)
            {
                ringkasanText += $"{counter}. {jasa.NamaJasa}\n";
                ringkasanText += $"   Jadwal: {jasa.TanggalJadwal.ToShortDateString()}\n";
                ringkasanText += $"   Harga: {jasa.TotalHarga.ToString("C0", new CultureInfo("id-ID"))}\n";
                counter++;
            }


            string totalFormatted = _totalHargaKeseluruhan.ToString("C0", new CultureInfo("id-ID"));
            
        }

        private void LoadKecamatan()
        {
            try
            {
                DataTable dtKecamatan = _controller.GetAllKecamatan();

                cmbKecamatan.DataSource = dtKecamatan;
                cmbKecamatan.DisplayMember = "nama_kecamatan";
                cmbKecamatan.ValueMember = "id_kecamatan";

                if (dtKecamatan.Rows.Count > 0)
                {
                    cmbKecamatan.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data Kecamatan: {ex.Message}", "Error Data Kecamatan");
            }
        }

        private void LoadMetodePembayaran()
        {
            cmbMetodePembayaran.Items.Clear();
            cmbMetodePembayaran.Items.Add("Tunai");
            cmbMetodePembayaran.Items.Add("Bank BCA");
            cmbMetodePembayaran.Items.Add("Bank BRI");
            cmbMetodePembayaran.Items.Add("Dana");
            cmbMetodePembayaran.SelectedIndex = 0;
        }

        private void cmbKecamatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKecamatan.SelectedValue != null && cmbKecamatan.SelectedValue is int)
            {
                LoadKelurahan();
            }
        }

        private void LoadKelurahan()
        {
            if (cmbKecamatan.SelectedValue == null || cmbKecamatan.SelectedValue is DataRowView)
            {
                cmbKelurahan.DataSource = null;
                cmbKelurahan.Items.Clear();
                return;
            }

            try
            {
                int idKecamatan = Convert.ToInt32(cmbKecamatan.SelectedValue);

        
                var listKelurahan = _controller.GetKelurahanByKecamatan(idKecamatan);

                cmbKelurahan.DataSource = listKelurahan;
                cmbKelurahan.DisplayMember = "nama_kelurahan";
                cmbKelurahan.ValueMember = "id_kelurahan";

                
                if (listKelurahan.Count > 0)
                {
                    cmbKelurahan.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data Kelurahan: {ex.Message}", "Error Data Kelurahan");
            }
        }

   

        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Batalkan pesanan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnLanjutPembayaran_Click_1(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(txtNamaPenerima.Text) ||
                    string.IsNullOrWhiteSpace(txtNomorHP.Text) ||
                    string.IsNullOrWhiteSpace(txtAlamatLengkap.Text) ||
                    cmbKecamatan.SelectedValue == null ||
                    cmbKelurahan.SelectedValue == null)
                {
                    MessageBox.Show("Lengkapi semua kolom input alamat dan penerima!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string metodeTerpilih = cmbMetodePembayaran.Text;
                string statusAwal = "diproses"; 

                CheckoutModel modelDasar = new CheckoutModel
                {
                    IdPengguna = _idPenggunaSaatIni,
                    NamaPenerima = txtNamaPenerima.Text,
                    NomorHP = txtNomorHP.Text,
                    AlamatLengkap = txtAlamatLengkap.Text,

                    IdKecamatan = Convert.ToInt32(cmbKecamatan.SelectedValue),
                    IdKelurahan = Convert.ToInt32(cmbKelurahan.SelectedValue),

                    NamaKecamatan = cmbKecamatan.Text,
                    NamaKelurahan = cmbKelurahan.Text,

                    TotalHarga = _totalHargaKeseluruhan,

                    MetodePembayaran = metodeTerpilih,
                    Status = statusAwal

                };

                try
                {
                    if (modelDasar.MetodePembayaran == "Tunai")
                    {
                        int idTransaksiBaru = _controller.InsertPemesananLengkap(modelDasar, _jasaDipilihList);
                        MessageBox.Show($"Pemesanan Tunai berhasil dicatat. ID Transaksi Utama: {idTransaksiBaru}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        FormCheckoutPembayaran formUpload = new FormCheckoutPembayaran(modelDasar, _jasaDipilihList);
                        if (formUpload.ShowDialog() == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal memproses pesanan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}